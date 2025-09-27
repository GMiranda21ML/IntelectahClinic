let agendamentos = [
    {
        id: 1,
        tipo: 'Consulta',
        especialidade: 'Cardiologia',
        medico: 'Dr. João Silva',
        data: '2024-02-15',
        horario: '14:30',
        local: 'Unidade Centro',
        endereco: 'Rua das Flores, 123 - Centro',
        status: 'confirmado'
    },
    {
        id: 2,
        tipo: 'Exame',
        especialidade: 'Hemograma Completo',
        medico: 'Laboratório',
        data: '2024-02-20',
        horario: '08:00',
        local: 'Unidade Norte',
        endereco: 'Av. Principal, 456 - Norte',
        status: 'confirmado'
    }
];

const especialidades = {
    consulta: [
        'Cardiologia',
        'Pediatria',
        'Ortopedia',
        'Dermatologia',
        'Ginecologia',
        'Neurologia',
        'Psiquiatria',
        'Oftalmologia'
    ],
    exame_laboratorial: [
        'Hemograma Completo',
        'Glicemia',
        'Colesterol',
        'Triglicerídeos',
        'TSH',
        'Vitamina D',
        'Ácido Úrico'
    ],
    exame_imagem: [
        'Raio-X',
        'Ultrassom',
        'Tomografia',
        'Ressonância Magnética',
        'Mamografia',
        'Ecocardiograma'
    ]
};

const unidades = [
    {
        nome: 'Unidade Centro',
        endereco: 'Rua das Flores, 123 - Centro'
    },
    {
        nome: 'Unidade Norte',
        endereco: 'Av. Principal, 456 - Norte'
    },
    {
        nome: 'Unidade Sul',
        endereco: 'Rua do Comércio, 789 - Sul'
    },
    {
        nome: 'Unidade Oeste',
        endereco: 'Av. das Palmeiras, 321 - Oeste'
    }
];

function iniciarAgendamento(tipo) {
    sessionStorage.setItem('tipoAgendamento', tipo);
    window.location.href = 'agendamento.html';
}

function carregarAgendamento() {
    const tipo = sessionStorage.getItem('tipoAgendamento') || 'consulta';
    document.getElementById('tipoSelecionado').textContent = 
        tipo === 'consulta' ? 'Consulta Médica' : 'Exame';
    
    carregarEspecialidades(tipo);
    carregarUnidades();
}

function carregarEspecialidades(tipo) {
    const container = document.getElementById('especialidadesContainer');
    const lista = tipo === 'consulta' ? especialidades.consulta : 
                  tipo === 'exame_laboratorial' ? especialidades.exame_laboratorial :
                  especialidades.exame_imagem;
    
    container.innerHTML = '';
    lista.forEach(especialidade => {
        const item = `
            <div class="col-md-6 mb-2">
                <button class="btn btn-outline-primary w-100" onclick="selecionarEspecialidade('${especialidade}')">
                    ${especialidade}
                </button>
            </div>
        `;
        container.innerHTML += item;
    });
}

function carregarUnidades() {
    const container = document.getElementById('unidadesContainer');
    container.innerHTML = '';
    
    unidades.forEach(unidade => {
        const item = `
            <div class="col-md-6 mb-2">
                <button class="btn btn-outline-primary w-100" onclick="selecionarUnidade('${unidade.nome}', '${unidade.endereco}')">
                    <strong>${unidade.nome}</strong><br>
                    <small>${unidade.endereco}</small>
                </button>
            </div>
        `;
        container.innerHTML += item;
    });
}

function selecionarTipo(tipo) {
    document.querySelectorAll('.tipo-btn').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');
    
    sessionStorage.setItem('tipoAgendamento', tipo);
    carregarEspecialidades(tipo);
    
    document.getElementById('especialidadeStep').style.display = 'block';
}

function selecionarEspecialidade(especialidade) {
    sessionStorage.setItem('especialidadeSelecionada', especialidade);
    document.getElementById('especialidadeSelecionada').textContent = especialidade;
    document.getElementById('localStep').style.display = 'block';
}

function selecionarUnidade(nome, endereco) {
    sessionStorage.setItem('unidadeSelecionada', nome);
    sessionStorage.setItem('enderecoSelecionado', endereco);
    document.getElementById('unidadeSelecionada').textContent = nome;
    document.getElementById('dataStep').style.display = 'block';
    carregarCalendario();
}

function carregarCalendario() {
    const container = document.getElementById('calendarioContainer');
    const hoje = new Date();
    const proximoMes = new Date(hoje.getFullYear(), hoje.getMonth() + 1, 0);
    
    container.innerHTML = '';
    
    for (let dia = 1; dia <= proximoMes.getDate(); dia++) {
        const data = new Date(hoje.getFullYear(), hoje.getMonth(), dia);
        const isPassado = data < hoje;
        const isFimSemana = data.getDay() === 0 || data.getDay() === 6;
        
        const dayElement = `
            <div class="calendar-day ${isPassado || isFimSemana ? 'disabled' : ''}" 
                 onclick="${!isPassado && !isFimSemana ? `selecionarData('${data.toISOString().split('T')[0]}')` : ''}">
                ${dia}
            </div>
        `;
        container.innerHTML += dayElement;
    }
}

function selecionarData(data) {
    document.querySelectorAll('.calendar-day').forEach(day => day.classList.remove('selected'));
    event.target.classList.add('selected');
    
    sessionStorage.setItem('dataSelecionada', data);
    document.getElementById('dataSelecionada').textContent = formatarData(data);
    document.getElementById('horarioStep').style.display = 'block';
    carregarHorarios();
}

function carregarHorarios() {
    const container = document.getElementById('horariosContainer');
    const horarios = ['08:00', '08:30', '09:00', '09:30', '10:00', '10:30', '14:00', '14:30', '15:00', '15:30', '16:00', '16:30'];
    
    container.innerHTML = '';
    horarios.forEach(horario => {
        const isOcupado = Math.random() > 0.7;
        const item = `
            <div class="col-md-3 mb-2">
                <button class="horario-slot ${isOcupado ? 'ocupado' : ''}" 
                        onclick="${!isOcupado ? `selecionarHorario('${horario}')` : ''}"
                        ${isOcupado ? 'disabled' : ''}>
                    ${horario}
                </button>
            </div>
        `;
        container.innerHTML += item;
    });
}

function selecionarHorario(horario) {
    document.querySelectorAll('.horario-slot').forEach(slot => slot.classList.remove('selected'));
    event.target.classList.add('selected');
    
    sessionStorage.setItem('horarioSelecionado', horario);
    document.getElementById('horarioSelecionado').textContent = horario;
    document.getElementById('confirmacaoStep').style.display = 'block';
    carregarResumo();
}

function carregarResumo() {
    const tipo = sessionStorage.getItem('tipoAgendamento');
    const especialidade = sessionStorage.getItem('especialidadeSelecionada');
    const unidade = sessionStorage.getItem('unidadeSelecionada');
    const endereco = sessionStorage.getItem('enderecoSelecionado');
    const data = sessionStorage.getItem('dataSelecionada');
    const horario = sessionStorage.getItem('horarioSelecionado');
    
    document.getElementById('resumoTipo').textContent = tipo === 'consulta' ? 'Consulta Médica' : 'Exame';
    document.getElementById('resumoEspecialidade').textContent = especialidade;
    document.getElementById('resumoData').textContent = formatarData(data);
    document.getElementById('resumoHorario').textContent = horario;
    document.getElementById('resumoUnidade').textContent = unidade;
    document.getElementById('resumoEndereco').textContent = endereco;
}

function confirmarAgendamento() {
    const novoAgendamento = {
        id: agendamentos.length + 1,
        tipo: sessionStorage.getItem('tipoAgendamento') === 'consulta' ? 'Consulta' : 'Exame',
        especialidade: sessionStorage.getItem('especialidadeSelecionada'),
        medico: 'A definir',
        data: sessionStorage.getItem('dataSelecionada'),
        horario: sessionStorage.getItem('horarioSelecionado'),
        local: sessionStorage.getItem('unidadeSelecionada'),
        endereco: sessionStorage.getItem('enderecoSelecionado'),
        status: 'confirmado'
    };
    
    agendamentos.push(novoAgendamento);
    

    sessionStorage.clear();
    
    alert('Agendamento confirmado com sucesso!');
    window.location.href = 'dashboard.html';
}

function filtrarEspecialidades() {
    const busca = document.getElementById('buscaEspecialidade').value.toLowerCase();
    const especialidades = document.querySelectorAll('#especialidadesContainer button');
    
    especialidades.forEach(btn => {
        const texto = btn.textContent.toLowerCase();
        if (texto.includes(busca)) {
            btn.parentElement.style.display = 'block';
        } else {
            btn.parentElement.style.display = 'none';
        }
    });
}

function formatarData(data) {
    const date = new Date(data);
    return date.toLocaleDateString('pt-BR');
}

function logout() {
    usuarioLogado = null;
    sessionStorage.clear();
    window.location.href = 'index.html';
}

document.addEventListener('DOMContentLoaded', function() {
    

     if (document.getElementById('especialidadesContainer')) {
        carregarAgendamento();
    }
});