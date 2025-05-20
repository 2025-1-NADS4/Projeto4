import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';
import Header from '../../components/Headeradmin/Header';
import ModalConfirmacao from '../../components/ModalConf/ModalConfirmacao';
import { useServicos } from '../../components/ServicosContext/ServicosContext';
import { API_URL } from '../../Config';
import './styles.css';

const Servicos = () => {
  const { servicos, setServicos } = useServicos();

  const [mostrarModal, setMostrarModal] = useState(false);
  const [mostrarConfirmacao, setMostrarConfirmacao] = useState(false);
  const [idParaExcluir, setIdParaExcluir] = useState(null);

  const [novoServico, setNovoServico] = useState({
    tradeName: '',
    status: 'Ativo',
    tipoCotacao: '',
  });

  const [appServices, setAppServices] = useState([]);
  const [appServicesSelecionados, setAppServicesSelecionados] = useState([]);
  const [novoAppServiceNome, setNovoAppServiceNome] = useState('');

  const [modoEdicao, setModoEdicao] = useState(false);
  const [idEdicao, setIdEdicao] = useState(null);

  useEffect(() => {
    fetch(`${API_URL}/CompanyRide`)
      .then(res => res.json())
      .then(data => setServicos(data))
      .catch(console.error);
  }, [setServicos]);

  useEffect(() => {
    fetch(`${API_URL}/AppService`)
      .then(res => res.json())
      .then(data => setAppServices(data))
      .catch(console.error);
  }, []);

  const abrirModal = () => setMostrarModal(true);

  const fecharModal = () => {
    setMostrarModal(false);
    setModoEdicao(false);
    setIdEdicao(null);
    setNovoServico({ tradeName: '', status: 'Ativo', tipoCotacao: '' });
    setAppServicesSelecionados([]);
    setNovoAppServiceNome('');
  };

  const handleChange = e => {
    const { name, value } = e.target;
    setNovoServico(prev => ({ ...prev, [name]: value }));
  };

  const adicionarAppServiceLocal = () => {
    const nome = novoAppServiceNome.trim();
    if (!nome) {
      alert('Informe um nome válido para o AppService');
      return;
    }
    setAppServicesSelecionados(prev => [...prev, { id: null, name: nome, novo: true }]);
    setNovoAppServiceNome('');
  };

  const toggleAppServiceSelecionado = (appService) => {
    const existe = appServicesSelecionados.find(a =>
      (a.id === appService.id && !a.novo) || (a.name === appService.name && a.novo)
    );
    if (existe) {
      setAppServicesSelecionados(prev =>
        prev.filter(a => !((a.id === appService.id && !a.novo) || (a.name === appService.name && a.novo)))
      );
    } else {
      setAppServicesSelecionados(prev => [...prev, appService]);
    }
  };

  const handleEditar = (servico) => {
    setModoEdicao(true);
    setIdEdicao(servico.id);
    setNovoServico({
      tradeName: servico.tradeName || '',
      status: servico.status || 'Ativo',
      tipoCotacao: servico.tipoCotacao || '',
    });

    // Buscar AppServices vinculados a esse CompanyRide
    fetch(`${API_URL}/CompanyRideAppService/${servico.id}`)
      .then(res => res.json())
      .then(vinculados => {
        const selecionados = appServices.filter(a =>
          vinculados.some(v => v.appServiceId === a.id)
        );
        setAppServicesSelecionados(selecionados);
        abrirModal();
      })
      .catch(err => {
        console.error('Erro ao buscar AppServices vinculados:', err);
        abrirModal(); // mesmo com erro, abre o modal para evitar bloqueio
      });
  };

  const handleExcluir = (id) => {
    setIdParaExcluir(id);
    setMostrarConfirmacao(true);
  };

  const confirmarExclusao = async () => {
    try {
      const res = await fetch(`${API_URL}/CompanyRide/${idParaExcluir}`, {
        method: 'DELETE',
      });

      if (res.ok) {
        setServicos(prev => prev.filter(s => s.id !== idParaExcluir));
      }
    } catch (err) {
      console.error('Erro ao excluir serviço:', err);
    }

    setMostrarConfirmacao(false);
    setIdParaExcluir(null);
  };

  const handleSalvar = async e => {
    e.preventDefault();

    const nomeValido = novoServico.tradeName.trim();
    if (!nomeValido) {
      alert('Preencha o nome corretamente.');
      return;
    }

    try {
      if (modoEdicao) {
        const res = await fetch(`${API_URL}/CompanyRide/${idEdicao}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            id: idEdicao,
            tradeName: nomeValido,
            status: novoServico.status,
            tipoCotacao: novoServico.tipoCotacao,
          }),
        });

        if (!res.ok) {
          alert('Erro ao atualizar serviço');
          return;
        }

        const atualizado = await res.json();
        setServicos(prev => prev.map(s => (s.id === idEdicao ? atualizado : s)));
      } else {
        const res = await fetch(`${API_URL}/CompanyRide`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(nomeValido),
        });

        if (!res.ok) {
          alert('Erro ao criar serviço');
          return;
        }

        const novo = await res.json();

        for (const appService of appServicesSelecionados) {
          if (appService.novo) {
            const createDto = {
              IdCompanyRide: novo.id,
              NameService: appService.name,
            };

            const resApp = await fetch(`${API_URL}/AppServices`, {
              method: 'POST',
              headers: { 'Content-Type': 'application/json' },
              body: JSON.stringify(createDto),
            });

            if (!resApp.ok) {
              console.error('Erro ao criar AppService:', appService.name);
              alert(`Erro ao criar AppService: ${appService.name}`);
            }
          } else {
            await fetch(`${API_URL}/CompanyRideAppService`, {
              method: 'POST',
              headers: { 'Content-Type': 'application/json' },
              body: JSON.stringify({ companyRideId: novo.id, appServiceId: appService.id }),
            });
          }
        }

        setServicos(prev => [...prev, novo]);
      }

      fecharModal();
    } catch (err) {
      console.error('Erro ao salvar serviço:', err);
      alert('Erro ao salvar serviço. Veja o console para detalhes.');
    }
  };

  return (
    <>
      <Header />
      <main className="empresas-container">
        <div className="empresas-card">
          <table className="empresas-table">
            <thead>
              <tr>
                <th>Nome</th>
                <th>Tipo de Cotação</th>
                <th>Status</th>
                <th className="acoes-coluna"></th>
              </tr>
            </thead>
            <tbody>
              {servicos.map(servico => (
                <tr key={servico.id}>
                  <td>{servico.tradeName}</td>
                  <td>
                    {servico.appServices && servico.appServices.length > 0
                      ? servico.appServices.map(a => a.nameService).join(', ')
                      : '-'}
                  </td>
                  <td>{servico.isActive ? 'Ativo' : 'Inativo'}</td>
                  <td className="acoes">
                    <button onClick={() => handleEditar(servico)} className="editar-btn">
                      <FaEdit />
                    </button>
                    <button onClick={() => handleExcluir(servico.id)} className="excluir-btn">
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>

          </table>

          <div className="novo-empresa-container">
            <button onClick={abrirModal} className="novo-empresa-btn">
              <FaPlus />
            </button>
          </div>
        </div>
      </main>

      {mostrarModal && (
        <div className="modal-overlay" onClick={fecharModal}>
          <div className="modal-content" onClick={e => e.stopPropagation()}>
            <h2>{modoEdicao ? 'Editar Serviço' : 'Novo Serviço'}</h2>
            <form onSubmit={handleSalvar}>
              <label>
                Nome
                <input
                  type="text"
                  name="tradeName"
                  value={novoServico.tradeName}
                  onChange={handleChange}
                  required
                />
              </label>

              <label>
                Status
                <select
                  name="status"
                  value={novoServico.status}
                  onChange={handleChange}
                >
                  <option value="Ativo">Ativo</option>
                  <option value="Inativo">Inativo</option>
                </select>
              </label>

              <fieldset style={{ marginTop: '10px' }}>
                <legend>AppServices vinculados</legend>

                {appServices.map(appService => {
                  const selecionado = appServicesSelecionados.some(a => a.id === appService.id && !a.novo);
                  return (
                    <label key={appService.id} style={{ display: 'block' }}>
                      <input
                        type="checkbox"
                        checked={selecionado}
                        onChange={() => toggleAppServiceSelecionado(appService)}
                      />
                      {appService.name}
                    </label>
                  );
                })}

                {appServicesSelecionados
                  .filter(a => a.novo)
                  .map((novoApp, idx) => (
                    <div key={`novo-${idx}`} style={{ marginTop: 5 }}>
                      <span>{novoApp.name} (Novo)</span>
                      <button
                        type="button"
                        onClick={() => setAppServicesSelecionados(prev => prev.filter(a => a !== novoApp))}
                        style={{ marginLeft: 10 }}
                      >
                        Remover
                      </button>
                    </div>
                  ))}

                <div style={{ marginTop: 10 }}>
                  <input
                    type="text"
                    placeholder="Novo AppService"
                    value={novoAppServiceNome}
                    onChange={e => setNovoAppServiceNome(e.target.value)}
                  />
                  <button type="button" onClick={adicionarAppServiceLocal} style={{ marginLeft: 5 }}>
                    Adicionar
                  </button>
                </div>
              </fieldset>

              <div style={{ marginTop: '15px', display: 'flex', justifyContent: 'flex-end' }}>
                <button type="button" onClick={fecharModal} style={{ marginRight: '10px' }}>
                  Cancelar
                </button>
                <button type="submit">Salvar</button>
              </div>
            </form>
          </div>
        </div>
      )}

      {mostrarConfirmacao && (
        <ModalConfirmacao
          mensagem="Tem certeza que deseja excluir este serviço?"
          onConfirmar={confirmarExclusao}
          onCancelar={() => setMostrarConfirmacao(false)}
        />
      )}
    </>
  );
};

export default Servicos;
