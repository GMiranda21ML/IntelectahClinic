const API_BASE_URL = 'http://localhost:5294/';
const AGENDAMENTO_CONTROLLER = 'Agendamento/';

async function carregarProximosAgendamentos() {
    const container = document.getElementById('proximosAgendamentos');
    container.innerHTML = 'Carregando agendamentos...';

    try {
        const response = await fetch(`${API_BASE_URL}${AGENDAMENTO_CONTROLLER}meus-agendamentos`, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            credentials: 'include'
        });

        if (!response.ok) {
            container.innerHTML = `<div class="alert alert-danger">Erro ao carregar agendamentos: ${response.statusText}</div>`;
            setTimeout(() => {
                window.location.href = 'index.html'; s
            }, 2000);
            return;
        } 

        const agendamentos = await response.json();

    
        const agora = new Date();
        const proximos = agendamentos.filter(a => a.status === 0 || a.status === 2);

        if (proximos.length === 0) {
            container.innerHTML = '<div class="alert alert-info">Nenhum agendamento futuro encontrado.</div>';
            return;
        }

        container.innerHTML = '';
        proximos.forEach(a => {
            const card = criarCardAgendamento(a);
            container.appendChild(card);
        });

    } catch (error) {
        console.error('Erro ao carregar próximos agendamentos:', error);
        container.innerHTML = '<div class="alert alert-danger">Erro de conexão com o servidor.</div>';
    }
}

function criarCardAgendamento(agendamento) {
    const card = document.createElement('div');
    card.className = 'card mb-3 shadow-sm';

    const tipoText = agendamento.especialidade.tipo === 0 ? 'Consulta' : 'Exame';
    const iconeTipo = agendamento.especialidade.tipo === 0 ? 'fas fa-user-md' : 'fas fa-flask';
    const dataHora = formatarDataHora(agendamento.dataHora);

    card.innerHTML = `
        <div class="card-body">
            <h5 class="card-title">${agendamento.especialidade.nomeEspecialidade} (${tipoText})</h5>
            <p class="card-text mb-1"><i class="fas fa-calendar me-2"></i>${dataHora}</p>
            <p class="card-text mb-1"><i class="fas fa-map-marker-alt me-2"></i>${agendamento.unidade.nomeUnidade}</p>
            <p class="card-text mb-2"><i class="${iconeTipo} me-2"></i>${tipoText}</p>
        </div>
    `;
    return card;
}

function formatarDataHora(dataHoraString) {
    if (!dataHoraString) return 'Não informado';
    const data = new Date(dataHoraString);
    const dia = String(data.getDate()).padStart(2, '0');
    const mes = String(data.getMonth() + 1).padStart(2, '0');
    const ano = data.getFullYear();
    const hora = String(data.getHours()).padStart(2, '0');
    const minuto = String(data.getMinutes()).padStart(2, '0');
    return `${dia}/${mes}/${ano} às ${hora}:${minuto}`;
}

function logout() {
    window.location.href = 'index.html';
}

async function verDetalhes(id) {

    alert('Ver detalhes do agendamento: ' + id);
}

window.onload = carregarProximosAgendamentos;
