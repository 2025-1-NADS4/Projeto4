import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';
import Header from '../../components/Headeradmin/Header';
import ModalConfirmacao from '../../components/ModalConf/ModalConfirmacao';
import './styles.css';
import axios from 'axios';
import { API_URL } from '../../Config';

const Empresas = () => {
  const [empresas, setEmpresas] = useState([]);
  const [servicos, setServicos] = useState([]);
  const [empresaExpandidaId, setEmpresaExpandidaId] = useState(null);

  const [mostrarModal, setMostrarModal] = useState(false);
  const [mostrarConfirmacao, setMostrarConfirmacao] = useState(false);
  const [idParaExcluir, setIdParaExcluir] = useState(null);

  const [novaEmpresa, setNovaEmpresa] = useState({
    nameService: '',
    cnpj: '',
    companyRideIds: []
  });

  const [modoEdicao, setModoEdicao] = useState(false);
  const [idEdicao, setIdEdicao] = useState(null);

  useEffect(() => {
    const fetchDados = async () => {
      try {
        const [empresasRes, servicosRes] = await Promise.all([
          axios.get(`${API_URL}/companies`),
          axios.get(`${API_URL}/CompanyRide`),
        ]);
        setEmpresas(empresasRes.data || []);
        setServicos(servicosRes.data || []);
      } catch (error) {
        console.error('Erro ao carregar dados:', error);
      }
    };
    fetchDados();
  }, []);

  // Função para obter os nomes dos serviços de uma empresa
  const getNomesServicos = (empresa) => {
  if (!empresa.companyCompanyRides || !Array.isArray(empresa.companyCompanyRides)) return [];

  return empresa.companyCompanyRides
    .map(cr => {
      const servico = servicos.find(s => s.id === cr.companyRideId);
      return servico ? servico.nameService || servico.tradeName : null;
    })
    .filter(Boolean);
};

  const abrirModal = () => setMostrarModal(true);

  const fecharModal = () => {
    setMostrarModal(false);
    setModoEdicao(false);
    setIdEdicao(null);
    setNovaEmpresa({ nameService: '', cnpj: '', companyRideIds: [] });
  };

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    
    if (type === 'checkbox') {
      setNovaEmpresa(prev => ({
        ...prev,
        companyRideIds: checked
          ? [...prev.companyRideIds, value]
          : prev.companyRideIds.filter(id => id !== value)
      }));
    } else {
      setNovaEmpresa(prev => ({ ...prev, [name]: value }));
    }
  };

  const handleSalvar = async (e) => {
    e.preventDefault();
    const { nameService, cnpj, companyRideIds } = novaEmpresa;

    if (!nameService.trim() || !cnpj.trim() || companyRideIds.length === 0) {
      alert('Por favor, preencha todos os campos e selecione ao menos um serviço.');
      return;
    }

    try {
      if (modoEdicao) {
        await axios.put(`${API_URL}/companies/${idEdicao}`, novaEmpresa);
        setEmpresas(prev =>
          prev.map(empresa =>
            empresa.id === idEdicao ? { ...empresa, ...novaEmpresa } : empresa
          )
        );
      } else {
        const res = await axios.post(`${API_URL}/companies`, novaEmpresa);
        setEmpresas(prev => [...prev, res.data]);
      }
      fecharModal();
    } catch (error) {
      console.error('Erro ao salvar empresa:', error);
      alert('Falha ao salvar empresa. Tente novamente.');
    }
  };

  const handleExcluir = (id) => {
    setIdParaExcluir(id);
    setMostrarConfirmacao(true);
  };

  const confirmarExclusao = async () => {
    try {
      await axios.delete(`${API_URL}/companies/${idParaExcluir}`);
      setEmpresas(prev => prev.filter(empresa => empresa.id !== idParaExcluir));
      setMostrarConfirmacao(false);
      setIdParaExcluir(null);
    } catch (error) {
      console.error('Erro ao excluir empresa:', error);
      alert('Falha ao excluir empresa. Tente novamente.');
    }
  };

  const cancelarExclusao = () => {
    setMostrarConfirmacao(false);
    setIdParaExcluir(null);
  };

  const handleEditar = (empresa) => {
    setNovaEmpresa({
      nameService: empresa.nameService,
      cnpj: empresa.cnpj,
      companyRideIds: empresa.companyCompanyRides.map(cr => cr.companyRideId)
    });
    setModoEdicao(true);
    setIdEdicao(empresa.id);
    abrirModal();
  };

  const toggleExpandirEmpresa = (id) => {
    setEmpresaExpandidaId(prev => (prev === id ? null : id));
  };

  return (
    <>
      <Header />
      <main className="empresas-container">
        <div className="empresas-card">
          <table className="empresas-table">
            <thead>
              <tr>
                <th>Razão Social</th>
                <th>CNPJ</th>
                <th>Serviços</th>
                <th>Status</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {empresas.length === 0 ? (
                <tr>
                  <td colSpan="5" style={{ textAlign: 'center' }}>
                    Nenhuma empresa cadastrada.
                  </td>
                </tr>
              ) : (
                empresas.map((empresa) => {
                  const nomesServicos = getNomesServicos(empresa);

                  return (
                    <tr key={empresa.id}>
                      <td onClick={() => toggleExpandirEmpresa(empresa.id)} style={{ cursor: 'pointer' }}>
                        {empresa.nameService}
                      </td>
                      <td>{empresa.cnpj}</td>
                      <td>
                        {nomesServicos.length > 0 ? nomesServicos.join(', ') : 'Nenhum serviço'}
                      </td>
                      <td>{empresa.isActive ? 'Ativo' : 'Inativo'}</td>
                      <td>
                        <button className="editar-btn" onClick={() => handleEditar(empresa)}>
                          <FaEdit />
                        </button>
                        <button className="excluir-btn" onClick={() => handleExcluir(empresa.id)}>
                          <FaTrash />
                        </button>
                      </td>
                    </tr>
                  );
                })
              )}
            </tbody>
          </table>

          <div className="novo-empresa-container">
            <button
              className="novo-empresa-btn"
              onClick={abrirModal}
              aria-label="Nova empresa"
            >
              <FaPlus />
            </button>
          </div>
        </div>
      </main>

      {mostrarModal && (
        <div className="modal-overlay" onClick={fecharModal}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <h2>{modoEdicao ? 'Editar Empresa' : 'Nova Empresa'}</h2>
            <form onSubmit={handleSalvar} className="modal-form">
              <label className="form-label">
                Razão Social
                <input
                  className="form-input"
                  name="nameService"
                  value={novaEmpresa.nameService}
                  onChange={handleChange}
                  required
                  autoFocus
                />
              </label>

              <label className="form-label">
                CNPJ
                <input
                  className="form-input"
                  name="cnpj"
                  value={novaEmpresa.cnpj}
                  onChange={handleChange}
                  required
                />
              </label>

              <label className="form-label">
                Serviços
                <div className="servicos-checkbox-group">
                  {servicos.length === 0 ? (
                    <p>Nenhum serviço cadastrado.</p>
                  ) : (
                    servicos.map((servico) => (
                      <label key={servico.id} className="checkbox-item">
                        <input
                          type="checkbox"
                          value={servico.id}
                          checked={novaEmpresa.companyRideIds.includes(servico.id)}
                          onChange={handleChange}
                        />
                        <span>{servico.nameService || servico.tradeName}</span>
                      </label>
                    ))
                  )}
                </div>
              </label>

              <div className="botoes-modal">
                <button type="submit" className="botao-submit">
                  {modoEdicao ? 'Salvar Alterações' : 'Salvar'}
                </button>
                <button type="button" className="botao-cancelar" onClick={fecharModal}>
                  Cancelar
                </button>
              </div>
            </form>
          </div>
        </div>
      )}

      {mostrarConfirmacao && (
        <ModalConfirmacao onConfirmar={confirmarExclusao} onCancelar={cancelarExclusao} />
      )}
    </>
  );
};

export default Empresas;