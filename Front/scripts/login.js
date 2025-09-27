const API_BASE_URL = 'http://localhost:5294/'; 

const USER_CONTROLLER = 'user/';


function mostrarLogin() {
    document.getElementById('loginForm').style.display = 'block';
    document.getElementById('cadastroForm').style.display = 'none';
    document.getElementById('feedbackMessage').style.display = 'none'; 
}

function mostrarCadastro() {
    document.getElementById('loginForm').style.display = 'none';
    document.getElementById('cadastroForm').style.display = 'block';
    document.getElementById('feedbackMessage').style.display = 'none'; 
}

function mostrarFeedback(message, isSuccess = true) {
    const feedbackDiv = document.getElementById('feedbackMessage');
    feedbackDiv.textContent = message;
    feedbackDiv.className = `alert ${isSuccess ? 'alert-success' : 'alert-danger'} mt-3`;
    feedbackDiv.style.display = 'block';
}


async function fazerLogin() {
    const cpf = document.getElementById('cpfLogin').value.replace(/\D/g, ''); 
    const password = document.getElementById('senhaLogin').value;

    if (!cpf || !password) {
        mostrarFeedback('Por favor, preencha o CPF e a Senha.', false);
        return;
    }

    const loginData = {
        cpf: cpf,
        password: password
    };

    try {
        const response = await fetch(`${API_BASE_URL}${USER_CONTROLLER}login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData),
            credentials: 'include' 
        });

        if (response.ok) {
            mostrarFeedback('Login realizado com sucesso! Redirecionando...', true);
            
            setTimeout(() => {
                window.location.href = 'dashboard.html'; 
            }, 1000); 

        } else {
            const errorText = await response.text(); 
            mostrarFeedback(`Falha no Login. Erro: ${errorText || 'Credenciais inválidas.'}`, false);
        }
    } catch (error) {
        console.error('Erro ao conectar com a API:', error);
        mostrarFeedback('Erro de conexão com o servidor. Tente novamente.', false);
    }
}

async function fazerCadastro() {
    const nomeCompleto = document.getElementById('nomeCadastro').value;
    const cpf = document.getElementById('cpfCadastro').value.replace(/\D/g, '');
    const dataNascimento = document.getElementById('dataNascimento').value;
    const telefone = document.getElementById('telefone').value;
    const email = document.getElementById('emailCadastro').value;
    const convenio = document.getElementById('convenio').value;
    const password = document.getElementById('senhaCadastro').value;
    const passwordConfirmada = document.getElementById('confirmarSenha').value;

    if (!nomeCompleto || !cpf || !dataNascimento || !telefone || !email || !convenio || !password || !passwordConfirmada) {
        mostrarFeedback('Por favor, preencha todos os campos do formulário de cadastro.', false);
        return;
    }

    if (password !== passwordConfirmada) {
        mostrarFeedback('As senhas digitadas não são iguais. Por favor, verifique.', false);
        return;
    }

    const cadastroData = {
        nomeCompleto: nomeCompleto,
        cpf: cpf,
        dataNascimento: dataNascimento,
        telefone: telefone,
        email: email,
        convenio: convenio,
        password: password,
        passwordConfirmada: passwordConfirmada
    };
    
    const dataFormatada = new Date(dataNascimento).toISOString();
    cadastroData.dataNascimento = dataFormatada;

    try {
        const response = await fetch(`${API_BASE_URL}${USER_CONTROLLER}cadastro`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(cadastroData),
            credentials: 'include' 
        });

        if (response.ok) {
            mostrarFeedback('✅ Cadastro realizado com sucesso! Faça seu login.', true);
            setTimeout(() => {
                mostrarLogin();
            }, 1500);
        } else {
            const errorText = await response.text();
            mostrarFeedback(`❌ Falha no Cadastro. Erro: ${errorText || 'Ocorreu um erro desconhecido.'}`, false);
        }
    } catch (error) {
        console.error('Erro ao conectar com a API:', error);
        mostrarFeedback('Erro de conexão com o servidor. Verifique se o backend está rodando.', false);
    }
}

async function buscarDadosAutenticados(endpoint) {
    try {
        const response = await fetch(`${API_BASE_URL}${endpoint}`, {
            method: 'GET', 
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });

        if (response.status === 401) {
            console.error("Não autorizado (401). Cookie não enviado ou expirado.");
            window.location.href = 'index.html';
            return null;
        }

        if (!response.ok) {
            console.error(`Erro ao buscar dados: ${response.statusText}`);
            return null;
        }

        return await response.json();
    } catch (error) {
        console.error('Erro de rede/conexão:', error);
        return null;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const btnLogin = document.getElementById('btnLogin');
    if (btnLogin) {
        btnLogin.addEventListener('click', fazerLogin);
    }

    const btnCadastro = document.getElementById('btnCadastro');
    if (btnCadastro) {
        btnCadastro.addEventListener('click', fazerCadastro);
    }
    
    if (document.getElementById('loginForm') || document.getElementById('cadastroForm')) {
    }

});
