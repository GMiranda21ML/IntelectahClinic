const API_BASE_URL = 'http://localhost:5294/'; 

const PACIENTE_CONTROLLER = 'Paciente/';

async function carregarDadosPerfil() {
    const url = `${API_BASE_URL}${PACIENTE_CONTROLLER}`;
    const feedbackMessage = document.getElementById('feedbackMessage');
    const contentContainer = document.getElementById('perfilContent');

    if (feedbackMessage) feedbackMessage.style.display = 'none';

    try {
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include' 
        });

        if (response.ok) {
            const data = await response.json();
            preencherCampos(data);
            if (contentContainer) contentContainer.style.display = 'block';

        } else if (response.status === 401 || response.status === 403) {
            mostrarFeedback('Sessão expirada ou acesso não autorizado. Faça o login novamente.', false);
            setTimeout(() => {
                window.location.href = 'index.html'; 
            }, 2000);

        } else {
            const errorText = await response.text();
            mostrarFeedback(`Erro ao carregar dados: ${errorText || response.statusText}`, false);
        }

    } catch (error) {
        console.error('Erro de conexão ao carregar perfil:', error);
        mostrarFeedback('Erro de conexão com o servidor. Verifique a URL da API.', false);
    }
}

function preencherCampos(dados) {
    let dataFormatada = 'Não informado';
    if (dados.dataNascimento) {
        try {
            const dataSimples = dados.dataNascimento.split('T')[0];
            const [ano, mes, dia] = dataSimples.split('-');
            dataFormatada = `${dia}/${mes}/${ano}`;
        } catch (e) {
            dataFormatada = 'Data inválida';
            console.error("Erro ao formatar data:", e);
        }
    }
    
    let cpfFormatado = dados.cpf ? dados.cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4') : 'Não informado';
    
    document.getElementById('nomeCompleto').textContent = dados.nomeCompleto || 'Não informado';
    document.getElementById('dataNascimento').textContent = dataFormatada;
    document.getElementById('cpf').textContent = cpfFormatado;
    document.getElementById('email').textContent = dados.email || 'Não informado';
    document.getElementById('convenio').textContent = dados.convenio || 'Particular';
}

function mostrarFeedback(message, isSuccess = true) {
    let feedbackDiv = document.getElementById('feedbackMessage');
    if (!feedbackDiv) {
        feedbackDiv = document.createElement('div');
        feedbackDiv.id = 'feedbackMessage';
        feedbackDiv.className = 'alert mt-3';
        document.querySelector('.container.mt-4').prepend(feedbackDiv); 
    }

    feedbackDiv.textContent = message;
    feedbackDiv.className = `alert ${isSuccess ? 'alert-success' : 'alert-danger'} mt-3`;
    feedbackDiv.style.display = 'block';
}

function editarPerfil() {
    alert('Funcionalidade de Edição de Perfil a ser implementada.');
}

function logout() {
    window.location.href = 'index.html';
}

window.onload = carregarDadosPerfil;
