const API_BASE_URL = 'http://localhost:5294/'; 
const AGENDAMENTO_CONTROLLER = 'Agendamento/'; 

let todosAgendamentos = [];


async function carregarAgendamentos() {
    const url = `${API_BASE_URL}${AGENDAMENTO_CONTROLLER}meus-agendamentos`; 
    const feedbackMessage = document.getElementById('feedbackMessage');
    const agendamentoContainer = document.getElementById('meusAgendamentos');

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
            if (response.status === 204) {
                todosAgendamentos = [];
            } else {
                todosAgendamentos = await response.json();
            }
            
            
            filtrarAgendamentos();

        } else if (response.status === 401 || response.status === 403) {
            mostrarFeedback('Sessão expirada ou acesso não autorizado. Faça o login novamente.', false);
            setTimeout(() => {
                window.location.href = 'index.html'; 
            }, 2000);

        } else {
            const errorText = await response.text();
            mostrarFeedback(`Erro ao carregar agendamentos: ${errorText || response.statusText}`, false);
        }

    } catch (error) {
        console.error('Erro de conexão ao carregar agendamentos:', error);
        mostrarFeedback('Erro de conexão com o servidor. Verifique a URL da API.', false);
    }
}


function renderizarAgendamentos(agendamentosFiltrados) {
    const futurosContainer = document.getElementById('listaFuturos');
    const historicoContainer = document.getElementById('listaHistorico');

    futurosContainer.innerHTML = '';
    historicoContainer.innerHTML = '';

    const agora = new Date();
    const proximos = [];
    const historico = [];

    agendamentosFiltrados.forEach(agendamento => {
        if (agendamento.status === 0 || agendamento.status === 2) {
            proximos.push(agendamento);
        } else {
            historico.push(agendamento);
        }
    });

    
    if (proximos.length > 0) {
        proximos.forEach(a => futurosContainer.appendChild(criarAgendamentoCard(a, false)));
    } else {
        futurosContainer.innerHTML = '<div class="alert alert-info mb-0">Nenhum agendamento futuro encontrado.</div>';
    }

    
    if (historico.length > 0) {
        historico.forEach(a => historicoContainer.appendChild(criarAgendamentoCard(a, true)));
    } else {
        historicoContainer.innerHTML = '<div class="alert alert-secondary mt-3 mb-0">Nenhum registro no histórico.</div>';
    }
}



function criarAgendamentoCard(agendamento, isHistorico) {
    const item = document.createElement('div');
    item.className = 'agendamento-item'; 
    item.setAttribute('data-status', agendamento.status);
    item.setAttribute('data-tipo', agendamento.especialidade.tipo); 

    const [statusText, statusStyle, statusBadge] = formatarStatus(agendamento.status, isHistorico);
    const tipoText = agendamento.especialidade.tipo === 0 ? 'Consulta' : 'Exame';
    const iconeTipo = agendamento.especialidade.tipo === 0 ? 'fas fa-user-md' : 'fas fa-flask';
    const titulo = agendamento.especialidade.nomeEspecialidade || agendamento.observacoes.substring(0, 50) + '...'; 
    const dataHoraFormatada = formatarDataHora(agendamento.dataHora);
    const endereco = agendamento.unidade.endereco;

    item.innerHTML = `
        <div class="agendamento-header">
            <span class="agendamento-tipo">${tipoText}</span>
            <span class="agendamento-status" style="background: ${statusStyle}; color: ${statusBadge};">${statusText}</span>
        </div>
        <h6 class="fw-bold">${titulo}</h6>
        <p class="mb-1"><i class="fas fa-calendar me-2"></i>${dataHoraFormatada}</p>
        <p class="mb-1"><i class="fas fa-map-marker-alt me-2"></i>${agendamento.unidade.nomeUnidade}</p>
        <p class="mb-1"><i class="${iconeTipo} me-2"></i>${agendamento.especialidade.tipo === 0 ? 'Especialista' : 'Laboratório'}</p>
        <p class="mb-3 text-muted">${endereco}</p>
        <div class="d-flex gap-2">
            <button class="btn btn-outline-primary btn-sm" onclick="verDetalhes(${agendamento.id})">
                <i class="fas fa-eye me-1"></i>Ver Detalhes
            </button>
            ${criarBotoesAcao(agendamento, isHistorico)}
        </div>
    `;
    return item;
}
    

function criarBotoesAcao(agendamento, isHistorico) {
    let botoes = '';

    
    if (!isHistorico && (agendamento.status === 0 || agendamento.status === 2)) {
        botoes += `
            <button class="btn btn-outline-danger btn-sm" onclick="cancelarAgendamento(${agendamento.id})">
                <i class="fas fa-times me-1"></i>Cancelar
            </button>
        `;
    }

    
    if (!isHistorico && agendamento.status === 0) {
        botoes += `
            <button class="btn btn-outline-warning btn-sm" onclick="reagendarAgendamento(${agendamento.id})">
                <i class="fas fa-redo me-1"></i>Reagendar
            </button>
        `;
    }

    
    if (isHistorico) {
        if (agendamento.status === 3) {
        botoes += `
            <button class="btn btn-outline-secondary btn-sm" onclick="alert('Baixar comprovante ainda não implementado!')">
                <i class="fas fa-file-download me-1"></i>Baixar
            </button>
        `;
        }
    }

    return botoes;
}



function filtrarAgendamentos() {
    const filtroStatus = document.getElementById('filtroStatus').value;
    const filtroTipo = document.getElementById('filtroTipo').value;
    const busca = (document.getElementById('buscaAgendamento').value || '').toLowerCase();

    
    const statusMapFilter = {
        'agendado': [0],
        'confirmado': [0],   
        'pendente': [0],     
        'cancelado': [1],
        'reagendado': [2],
        'atendido': [3]
    };

    const agendamentosFiltrados = todosAgendamentos.filter(agendamento => {
        
        const status = typeof agendamento.status === 'number' ? agendamento.status : null;
        const nomeEsp = (agendamento.especialidade && agendamento.especialidade.nomeEspecialidade) ? agendamento.especialidade.nomeEspecialidade.toLowerCase() : '';
        const nomeUnidade = (agendamento.unidade && agendamento.unidade.nomeUnidade) ? agendamento.unidade.nomeUnidade.toLowerCase() : '';

        
        let statusMatch = true;
        if (filtroStatus !== 'todos') {
            const allowed = statusMapFilter[filtroStatus] || [];
            statusMatch = allowed.includes(status);
        }

        
        const tipoMatch = filtroTipo === 'todos' ||
            (agendamento.especialidade && agendamento.especialidade.tipo === 0 && filtroTipo === 'Consulta') ||
            (agendamento.especialidade && agendamento.especialidade.tipo === 1 && filtroTipo === 'Exame');

        
        const buscaMatch = nomeEsp.includes(busca) || nomeUnidade.includes(busca);

        return statusMatch && tipoMatch && buscaMatch;
    });

    renderizarAgendamentos(agendamentosFiltrados);
}
    
    

async function verDetalhes(id) {
    const agendamento = todosAgendamentos.find(a => a.id === id);
    if (!agendamento) {
        alert('Agendamento não encontrado!');
        return;
    }

    const tipoText = agendamento.especialidade.tipo === 0 ? 'Consulta' : 'Exame';
    const especialidadeText = agendamento.especialidade.nomeEspecialidade;
    const dataHoraFormatada = formatarDataHora(agendamento.dataHora);
    const [statusText] = formatarStatus(agendamento.status, false); 

    document.getElementById('modalTipo').textContent = tipoText;
    document.getElementById('modalEspecialidade').textContent = especialidadeText;
    document.getElementById('modalDataHorario').textContent = dataHoraFormatada;
    document.getElementById('modalStatus').textContent = statusText;
    document.getElementById('modalLocal').textContent = agendamento.unidade.nomeUnidade;
    document.getElementById('modalEndereco').textContent = agendamento.unidade.endereco;

    
    const detalhesModal = new bootstrap.Modal(document.getElementById('detalhesModal'));
    detalhesModal.show();
}

async function cancelarAgendamento(id) {
    if (!confirm('Tem certeza que deseja cancelar este agendamento?')) {
        return;
    }

    const url = `${API_BASE_URL}${AGENDAMENTO_CONTROLLER}cancelar/${id}`;

    try {
        const response = await fetch(url, {
            method: 'POST', 
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include' 
        });

        if (response.ok) {
            mostrarFeedback('Agendamento cancelado com sucesso!', true);
            
            carregarAgendamentos(); 
        } else {
            const errorText = await response.text();
            mostrarFeedback(`Erro ao cancelar agendamento: ${errorText || response.statusText}`, false);
        }
    } catch (error) {
        console.error('Erro de conexão ao cancelar agendamento:', error);
        mostrarFeedback('Erro de conexão com o servidor.', false);
    }
}
    
function reagendarAgendamento(id) {
    const agendamento = todosAgendamentos.find(a => a.id === id);
    if (!agendamento) {
        alert('Agendamento não encontrado!');
        return;
    }

    
    document.getElementById('reagendarId').value = agendamento.id;

    
    if (agendamento.dataHora) {
        const data = new Date(agendamento.dataHora);
        const localISO = data.toISOString().slice(0,16); 
        document.getElementById('novaDataHora').value = localISO;
    }

    
    const modal = new bootstrap.Modal(document.getElementById('reagendarModal'));
    modal.show();
}

async function confirmarReagendamento() {
    const id = document.getElementById('reagendarId').value;
    const novaDataHora = document.getElementById('novaDataHora').value;

    if (!novaDataHora) {
        alert('Por favor, selecione a nova data e hora.');
        return;
    }

    const url = `${API_BASE_URL}${AGENDAMENTO_CONTROLLER}reagendar`;
    const body = {
        id: parseInt(id),
        novaDataHora: new Date(novaDataHora).toISOString()
    };

    try {
        const response = await fetch(url, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(body),
            credentials: 'include'
        });

        if (response.ok) {
            mostrarFeedback('Agendamento reagendado com sucesso!', true);
            const modal = bootstrap.Modal.getInstance(document.getElementById('reagendarModal'));
            modal.hide();
            carregarAgendamentos();
        } else {
            const errorText = await response.text();
            mostrarFeedback(`Erro ao reagendar: ${errorText || response.statusText}`, false);
        }
    } catch (error) {
        console.error('Erro de conexão ao reagendar:', error);
        mostrarFeedback('Erro de conexão com o servidor.', false);
    }
}

function formatarDataHora(dataHoraString) {
    if (!dataHoraString) return 'Não informado';
    try {
        const data = new Date(dataHoraString);
        const dia = String(data.getDate()).padStart(2, '0');
        const mes = String(data.getMonth() + 1).padStart(2, '0');
        const ano = data.getFullYear();
        const hora = String(data.getHours()).padStart(2, '0');
        const minuto = String(data.getMinutes()).padStart(2, '0');
        return `${dia}/${mes}/${ano} às ${hora}:${minuto}`;
    } catch (e) {
        console.error("Erro ao formatar data:", e);
        return 'Data inválida';
    }
}

function formatarStatus(status, isHistorico) {
    const statusMap = {
        0: ['agendado',   'rgba(13, 202, 240, 0.1)', '#0dcaf0'], 
        1: ['cancelado',  'rgba(220, 53, 69, 0.1)', '#dc3545'],  
        2: ['reagendado', 'rgba(255, 193, 7, 0.1)', '#ffc107'],  
        3: ['atendido',   'rgba(40, 167, 69, 0.1)', '#28a745']   
    };

    
    if (isHistorico && status === 0) {
            return ['não compareceu', 'rgba(255, 193, 7, 0.1)', '#ffc107']; 
    }

    const [text, bg, badge] = statusMap[status] || ['pendente', 'rgba(255, 193, 7, 0.1)', '#ffc107']; 
    return [text, bg, badge];
}

function mostrarFeedback(message, isSuccess = true) {
    let feedbackDiv = document.getElementById('feedbackMessage');
    if (!feedbackDiv) {
        feedbackDiv = document.createElement('div');
        feedbackDiv.id = 'feedbackMessage';
        
        document.querySelector('.container.mt-4').prepend(feedbackDiv); 
    }

    feedbackDiv.textContent = message;
    feedbackDiv.className = `alert ${isSuccess ? 'alert-success' : 'alert-danger'} mt-3`;
    feedbackDiv.style.display = 'block';
}

function logout() {
    window.location.href = 'index.html';
}


window.onload = carregarAgendamentos;
