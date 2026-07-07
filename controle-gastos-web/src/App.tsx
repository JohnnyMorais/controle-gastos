import { useState, useEffect } from 'react';

import { api } from './services/api';

import type { Person, Summary } from './types/index';

import './App.css'; //  Pode manter o CSS padrão do Vite ou customizar depois
 
function App() {

  const [people, setPeople] = useState<Person[]>([]);

  const [summary, setSummary] = useState<Summary | null>(null);
 
  // Estados dos formulários

  const [personName, setPersonName] = useState('');

  const [personAge, setPersonAge] = useState('');

  const [transDesc, setTransDesc] = useState('');

  const [transValue, setTransValue] = useState('');

  const [transType, setTransType] = useState(0);

  const [transPersonId, setTransPersonId] = useState(0);
 
  // Carrega os dados iniciais

  const loadData = async () => {

    try {

      const [peopleRes, summaryRes] = await Promise.all([

        api.get('/People'),

        api.get('/Summary')

      ]);

      setPeople(peopleRes.data);

      setSummary(summaryRes.data);

    } catch (error) {

      console.error("Erro ao carregar dados:", error);

    }

  };
 
  useEffect(() => {

    loadData();

  }, []);
 
  // Funções de Cadastro e Deleção

  const handleCreatePerson = async (e: React.FormEvent) => {

    e.preventDefault();

    await api.post('/People', { name: personName, age: Number(personAge) });

    setPersonName('');

    setPersonAge('');

    loadData();

  };
 
  const handleDeletePerson = async (id: number) => {

    await api.delete(`/People/${id}`);

    loadData();

  };
 
  const handleCreateTransaction = async (e: React.FormEvent) => {

    e.preventDefault();

    try {

      await api.post('/Transactions', {

        description: transDesc,

        value: Number(transValue),

        type: Number(transType),

        personId: Number(transPersonId)

      });

      setTransDesc('');

      setTransValue('');

      loadData();

    } catch (error: any) {

      alert(error.response?.data || "Erro ao cadastrar transação.");

    }

  };
 
  // R.P. - Lógica visual para travar a seleção de receita para menores

  const selectedPerson = people.find(p => p.id === Number(transPersonId));

  const isMinor = selectedPerson ? selectedPerson.age < 18 : false;
 
  useEffect(() => {

    if (isMinor && transType === 1) {

      setTransType(0); // Força para despesa se tentar burlar

    }

  }, [isMinor, transType]);
 
  return (
<div style={{ padding: '20px', maxWidth: '800px', margin: '0 auto', fontFamily: 'sans-serif' }}>
<h1>Controle de Gastos Residenciais</h1>
 
      <hr />
 
      {/* CADASTRO DE PESSOAS */}
<section>
<h2>Cadastro de Pessoa</h2>
<form onSubmit={handleCreatePerson} style={{ display: 'flex', gap: '10px', marginBottom: '10px' }}>
<input required placeholder="Nome" value={personName} onChange={e => setPersonName(e.target.value)} />
<input required type="number" min="0" placeholder="Idade" value={personAge} onChange={e => setPersonAge(e.target.value)} />
<button type="submit">Cadastrar Pessoa</button>
</form>
</section>
 
      <hr />
 
      {/* CADASTRO DE TRANSAÇÕES */}
<section>
<h2>Cadastro de Transação</h2>
<form onSubmit={handleCreateTransaction} style={{ display: 'flex', gap: '10px', flexDirection: 'column' }}>
<select required value={transPersonId} onChange={e => setTransPersonId(Number(e.target.value))}>
<option value={0} disabled>Selecione uma Pessoa</option>

            {people.map(p => (
<option key={p.id} value={p.id}>{p.name} ({p.age} anos)</option>

            ))}
</select>
<input required placeholder="Descrição" value={transDesc} onChange={e => setTransDesc(e.target.value)} />
<input required type="number" step="0.01" min="0.01" placeholder="Valor (R$)" value={transValue} onChange={e => setTransValue(e.target.value)} />
<select value={transType} onChange={e => setTransType(Number(e.target.value))} disabled={isMinor}>
<option value={0}>Despesa</option>
<option value={1} disabled={isMinor}>Receita</option>
</select>

          {isMinor && <small style={{ color: 'red' }}>Menores de 18 anos só podem registrar despesas.</small>}
 
          <button type="submit" disabled={!transPersonId}>Cadastrar Transação</button>
</form>
</section>
 
      <hr />
 
      {/* CONSULTA DE TOTAIS */}
<section>
<h2>Consulta de Totais</h2>
<table border={1} cellPadding={8} style={{ width: '100%', borderCollapse: 'collapse', textAlign: 'left' }}>
<thead>
<tr>
<th>Nome</th>
<th>Receitas</th>
<th>Despesas</th>
<th>Saldo</th>
<th>Ações</th>
</tr>
</thead>
<tbody>

            {summary?.pessoas.map(p => (
<tr key={p.id}>
<td>{p.name}</td>
<td style={{ color: 'green' }}>R$ {p.totalReceitas.toFixed(2)}</td>
<td style={{ color: 'red' }}>R$ {p.totalDespesas.toFixed(2)}</td>
<td><strong>R$ {p.saldo.toFixed(2)}</strong></td>
<td>
<button onClick={() => handleDeletePerson(p.id)} style={{ cursor: 'pointer' }}>Excluir</button>
</td>
</tr>

            ))}
</tbody>
</table>
 
        {summary && (
<div style={{ marginTop: '20px', padding: '15px', backgroundColor: '#f0f0f0', borderRadius: '5px', color: '#333' }}>
<h3>Total Geral</h3>
<p><strong>Receitas Totais:</strong> R$ {summary.totalGeral.totalReceitas.toFixed(2)}</p>
<p><strong>Despesas Totais:</strong> R$ {summary.totalGeral.totalDespesas.toFixed(2)}</p>
<p><strong>Saldo Líquido:</strong> R$ {summary.totalGeral.saldoLiquido.toFixed(2)}</p>
</div>

        )}
</section>
<footer style={{ marginTop: '40px', textAlign: 'center', fontSize: '0.8rem', color: '#666' }}>

        Desenvolvido por Johnny Morais.
</footer>
</div>

  );

}
 
export default App;
 