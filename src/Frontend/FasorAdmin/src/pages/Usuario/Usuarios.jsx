import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaPlus } from 'react-icons/fa';
import Style from './styles.css';
import Header from '../../components/Headeradmin/Header';
import ModalConfirmacao from '../../components/ModalConf/ModalConfirmacao';
import { API_URL } from '../../Config';

const Usuarios = () => {
  const [usuarios, setUsuarios] = useState([]);
  const [mostrarModal, setMostrarModal] = useState(false);
  const [mostrarConfirmacao, setMostrarConfirmacao] = useState(false);
  const [idParaExcluir, setIdParaExcluir] = useState(null);

  const [novoUsuario, setNovoUsuario] = useState({
    Name: '',
    Surname: '',
    cpf: '',
    email: '',
    status: 'Ativo',
    CompanyId: '',
  });

  const [modoEdicao, setModoEdicao] = useState(false);
  const [idEdicao, setIdEdicao] = useState(null);
  const [empresas, setEmpresas] = useState([]);

  const carregarUsuarios = async () => {
    try {
      console.log("URL sendo chamada:", `${API_URL}/users`); 
      const resposta = await fetch(`${API_URL}/users`);
      console.log("Resposta bruta:", resposta); 
      const dados = await resposta.json();
      console.log("Dados recebidos:", dados);
      setUsuarios(dados);
    } catch (error) {
      console.error('Erro ao carregar usuários:', error);
    }
  };

  const carregarEmpresas = async () => {
    try {
      const resposta = await fetch(`${API_URL}/companies`);
      const dados = await resposta.json();
      setEmpresas(dados);
    } catch (error) {
      console.error('Erro ao carregar empresas:', error);
    }
  };

  useEffect(() => {
    carregarUsuarios();
    carregarEmpresas();
  }, []);

  const abrirModal = () => setMostrarModal(true);

  const fecharModal = () => {
    setMostrarModal(false);
    setModoEdicao(false);
    setIdEdicao(null);
    setNovoUsuario({
      Name: '',
      Surname: '',
      cpf: '',
      email: '',
      status: 'Ativo',
      CompanyId: '',
    });
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNovoUsuario((prev) => ({ ...prev, [name]: value }));
  };

  const handleSalvar = async (e) => {
    e.preventDefault();

    const usuarioPayload = {
      ...novoUsuario,
      status: novoUsuario.status === 'Ativo',
      // CompanyId já está vindo certo no state
    };

    try {
      const resposta = await fetch(
        `${API_URL}/users${modoEdicao ? `/${idEdicao}` : ''}`,
        {
          method: modoEdicao ? 'PUT' : 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(usuarioPayload),
        }
      );

      if (!resposta.ok) {
        const erro = await resposta.text();
        alert(`Erro ao salvar usuário: ${erro}`);
        return;
      }

      carregarUsuarios();
      fecharModal();
    } catch (error) {
      alert('Erro ao salvar usuário.');
      console.error(error);
    }
  };

  const handleExcluir = (id) => {
    setIdParaExcluir(id);
    setMostrarConfirmacao(true);
  };

  const confirmarExclusao = async () => {
    try {
      const resposta = await fetch(`${API_URL}/users/${idParaExcluir}`, {
        method: 'DELETE',
      });

      if (!resposta.ok) {
        const erro = await resposta.text();
        alert(`Erro ao excluir usuário: ${erro}`);
        return;
      }

      carregarUsuarios();
      setMostrarConfirmacao(false);
    } catch (error) {
      alert('Erro ao excluir usuário.');
      console.error(error);
    }
  };

  const cancelarExclusao = () => {
    setMostrarConfirmacao(false);
    setIdParaExcluir(null);
  };

  const handleEditar = (usuario) => {
    setNovoUsuario({
      Name: usuario.Name || '',
      Surname: usuario.Surname || '',
      cpf: usuario.cpf || '',
      email: usuario.email || '',
      CompanyId: usuario.CompanyId || '',
    });
    setModoEdicao(true);
    setIdEdicao(usuario.id);
    abrirModal();
  };

  return (
    <>
      <Header />
      <main className="usuarios-container">
        <div className="usuarios-card">
          <table className="usuarios-table">
            <thead>
              <tr>
                <th>Nome</th>
                <th>CPF</th>
                <th>Email</th>
                <th>Empresa</th>
                <th className="acoes-coluna"></th>
              </tr>
            </thead>
            <tbody>
              {usuarios.map((usuario) => (
                <tr key={usuario.id}>
                  <td>{usuario.name} {usuario.surname}</td>
                  <td>{usuario.cpf}</td>
                  <td>{usuario.email}</td>
                  <td>{usuario.companyName || '—'}</td>
                  <td className="acoes">
                    <button className="editar-btn" onClick={() => handleEditar(usuario)}>
                      <FaEdit />
                    </button>
                    <button className="excluir-btn" onClick={() => handleExcluir(usuario.id)}>
                      <FaTrash />
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          <div className="novo-usuario-container">
            <button className="novo-usuario-btn" onClick={abrirModal}>
              <FaPlus />
            </button>
          </div>
        </div>
      </main>

      {mostrarModal && (
        <div className="modal-overlay" onClick={fecharModal}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <h2>{modoEdicao ? 'Editar Usuário' : 'Novo Usuário'}</h2>
            <form onSubmit={handleSalvar}>
              <label>
                Nome
                <input type="text" name="Name" value={novoUsuario.Name} onChange={handleChange} />
              </label>
              <label>
                Sobrenome
                <input type="text" name="Surname" value={novoUsuario.Surname} onChange={handleChange} />
              </label>
              <label>
                CPF
                <input type="text" name="cpf" value={novoUsuario.cpf} onChange={handleChange} />
              </label>
              <label>
                Email
                <input type="email" name="email" value={novoUsuario.email} onChange={handleChange} />
              </label>
              <label>
                Empresa
                <select name="CompanyId" value={novoUsuario.CompanyId} onChange={handleChange}>
                  <option value="">Selecione uma empresa</option>
                  {empresas.map((empresa) => (
                    <option key={empresa.id} value={empresa.id}>
                      {empresa.tradeName || empresa.nameService || 'Empresa sem nome'}
                    </option>
                  ))}
                </select>
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
        <ModalConfirmacao
          titulo="Excluir Usuário"
          mensagem="Tem certeza que deseja excluir este usuário?"
          onConfirmar={confirmarExclusao}
          onCancelar={cancelarExclusao}
        />
      )}
    </>
  );
};

export default Usuarios;
