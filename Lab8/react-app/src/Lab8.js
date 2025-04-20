import React, { useState } from 'react';
import './Lab8.css';

const Lab8 = () => {
  const [inputSequences, setInputSequences] = useState('');
  const [grammar, setGrammar] = useState(null);
  const [generatedSequence, setGeneratedSequence] = useState('');
  const [loading, setLoading] = useState(false);

  const analyzeSequences = () => {
    setLoading(true);
    setTimeout(() => {
      const sequences = inputSequences.split('\n').filter(s => s.trim() !== '');
      if (sequences.length === 0) {
        alert('Пожалуйста, введите хотя бы одну последовательность терминалов');
        setLoading(false);
        return;
      }
      const nonRecursiveGrammar = buildNonRecursiveGrammar(sequences);
      const recursiveGrammar = makeGrammarRecursive(nonRecursiveGrammar);
      const simplifiedGrammar = simplifyGrammar(recursiveGrammar);
      setGrammar(simplifiedGrammar);
      setLoading(false);
    }, 1000);
  };

  const generateNewSequence = () => {
    if (!grammar) {
      alert('Сначала создайте грамматику, нажав "Анализ последовательностей"');
      return;
    }
    const sequence = generateFromGrammar(grammar);
    setGeneratedSequence(sequence);
  };

  // Вспомогательные функции (заглушки - в реальном приложении нужно реализовать алгоритмы)
  const buildNonRecursiveGrammar = (sequences) => {
    
    // 1. Сортируем последовательности по убыванию длины
    const sortedSequences = [...sequences].sort((a, b) => b.length - a.length);

    const rules = [];
    let nextNonTerminalIndex = 1;
  
    // 2. Обрабатываем каждую последовательность
    for (const sequence of sortedSequences) {
      let currentSequence = sequence;
      let currentRules = [];
      let lastNonTerminal = null;
  
      // 3. Разбиваем последовательность на правила
      while (currentSequence.length > 2) {
        const firstChar = currentSequence[0];
        const remaining = currentSequence.slice(1);
        
        const nonTerminal = `A${nextNonTerminalIndex++}`;
        currentRules.push({
          from: lastNonTerminal || 'S',
          to: `${firstChar} ${nonTerminal}`
        });
        
        lastNonTerminal = nonTerminal;
        currentSequence = remaining;
      }
  
      // 4. Добавляем остаточное правило (длины 2)
      if (currentSequence.length === 2) {
        currentRules.push({
          from: lastNonTerminal || 'S',
          to: `${currentSequence[0]} ${currentSequence[1]}`
        });
      } else if (currentSequence.length === 1) {
        // Если последовательность нечетной длины, добавляем правило для последнего символа
        currentRules.push({
          from: lastNonTerminal || 'S',
          to: currentSequence[0]
        });
      }
  
      // 5. Добавляем все правила для этой последовательности
      rules.push(...currentRules);
    }
  
    return {
      type: 'non-recursive',
      sequences: sequences,
      rules: rules,
    };
  };

  const makeGrammarRecursive = (grammar) => {
    // 1. Создаем копию правил из нерекурсивной грамматики
    const rules = [...grammar.rules];
    const recursiveRules = [];
    const mergedNonTerminals = {};
  
    // 2. Находим все остаточные правила (длины 2)
    const residualRules = rules.filter(rule => 
      rule.to.split(' ').length === 2 && 
      !rule.to.split(' ').some(sym => sym.startsWith('A'))
    );
  
    // 3. Для каждого остаточного правила ищем подходящие неостаточные правила для слияния
    for (const residualRule of residualRules) {
      const [firstSym, secondSym] = residualRule.to.split(' ');
      const residualNonTerminal = residualRule.from;
  
      // Ищем правило вида A_n → firstSym A_m
      const candidateRule = rules.find(rule => 
        rule.to.startsWith(`${firstSym} A`) && 
        rule.to.split(' ')[1] !== residualNonTerminal
      );
  
      if (candidateRule) {
        const candidateNonTerminal = candidateRule.from;
        // Заменяем все вхождения residualNonTerminal на candidateNonTerminal
        mergedNonTerminals[residualNonTerminal] = candidateNonTerminal;
      }
    }
  
    // 4. Создаем новые правила с учетом слияний
    for (const rule of rules) {
      // Пропускаем остаточные правила, которые будем заменять
      if (residualRules.includes(rule)) continue;
  
      let newRule = {...rule};
      
      // Заменяем нетерминалы, если они были слиты
      if (mergedNonTerminals[rule.from]) {
        newRule.from = mergedNonTerminals[rule.from];
      }
      
      // Заменяем нетерминалы в правой части
      const rhsSymbols = newRule.to.split(' ');
      newRule.to = rhsSymbols.map(sym => 
        mergedNonTerminals[sym] || sym
      ).join(' ');
  
      recursiveRules.push(newRule);
    }
  
    // 5. Добавляем рекурсивные правила на основе слияний
    for (const [from, to] of Object.entries(mergedNonTerminals)) {
      // Находим символы из остаточного правила
      const residualRule = residualRules.find(r => r.from === from);
      const [firstSym, secondSym] = residualRule.to.split(' ');
      
      // Добавляем рекурсивное правило
      recursiveRules.push({
        from: to,
        to: `${firstSym} ${to}`
      });
      
      // Добавляем терминальное правило
      recursiveRules.push({
        from: to,
        to: secondSym
      });
    }
  
    return {
      ...grammar,
      type: 'recursive',
      rules: recursiveRules,
    };
  };

  const simplifyGrammar = (grammar) => {
    const rules = [...grammar.rules];
    const simplifiedRules = [];
    const equivalentNonTerminals = {};
  
    // 1. Группируем правила по левой части
    const ruleGroups = {};
    for (const rule of rules) {
      if (!ruleGroups[rule.from]) {
        ruleGroups[rule.from] = [];
      }
      ruleGroups[rule.from].push(rule.to);
    }
  
    // 2. Находим эквивалентные нетерминалы (которые порождают одинаковые цепочки)
    const nonTerminals = Object.keys(ruleGroups);
    for (let i = 0; i < nonTerminals.length; i++) {
      const nt1 = nonTerminals[i];
      if (equivalentNonTerminals[nt1]) continue; // Уже обработан
      
      for (let j = i + 1; j < nonTerminals.length; j++) {
        const nt2 = nonTerminals[j];
        if (equivalentNonTerminals[nt2]) continue;
        
        // Сравниваем множества правых частей
        const set1 = new Set(ruleGroups[nt1]);
        const set2 = new Set(ruleGroups[nt2]);
        
        if (set1.size === set2.size && 
            [...set1].every(item => set2.has(item))) {
          equivalentNonTerminals[nt2] = nt1;
        }
      }
    }
  
    // 3. Создаем новые правила с заменой эквивалентных нетерминалов
    const usedNonTerminals = new Set();
    for (const rule of rules) {
      // Пропускаем правила с эквивалентными нетерминалами
      if (equivalentNonTerminals[rule.from]) continue;
      
      // Заменяем нетерминалы в правой части
      const rhsParts = rule.to.split(' ');
      const newRhs = rhsParts.map(part => 
        equivalentNonTerminals[part] || part
      ).join(' ');
      
      // Если такого правила еще нет - добавляем
      if (!usedNonTerminals.has(rule.from)) {
        // Объединяем все правила с одинаковой левой частью
        const allOptions = ruleGroups[rule.from]
          .map(option => 
            option.split(' ')
              .map(part => equivalentNonTerminals[part] || part)
              .join(' ')
          )
          .filter((value, index, self) => self.indexOf(value) === index); // Уникальные
          
        simplifiedRules.push({
          from: rule.from,
          to: allOptions.join(' | ')
        });
        
        usedNonTerminals.add(rule.from);
      }
    }
  
    // 4. Удаляем дубликаты и сортируем правила
    const uniqueRules = [];
    const seen = new Set();
    
    for (const rule of simplifiedRules) {
      const key = `${rule.from}->${rule.to}`;
      if (!seen.has(key)) {
        seen.add(key);
        uniqueRules.push(rule);
      }
    }
  
    // Сортируем: сначала S, потом остальные
    uniqueRules.sort((a, b) => {
      if (a.from === 'S') return -1;
      if (b.from === 'S') return 1;
      return a.from.localeCompare(b.from);
    });
  
    return {
      ...grammar,
      type: 'simplified',
      rules: uniqueRules,
    };
  };
  

  const generateFromGrammar = (grammar) => {
    const rules = grammar.rules;
    const ruleMap = {};
    
    for (let rule of rules) {
      if (!ruleMap[rule.from]) ruleMap[rule.from] = [];
      rule.to.split('|').forEach(option => {
        ruleMap[rule.from].push(option.trim());
      });
    }
  
    if (!ruleMap['S']) {
      return ''; 
    }
  
    const expand = (symbol) => {
      if (!/^[A-Z]/.test(symbol)) {
        return symbol;
      }

      if (!ruleMap[symbol] || ruleMap[symbol].length === 0) {
        return '';
      }
  
      const options = ruleMap[symbol];

      const terminalOptions = options.filter(opt => 
        opt.split(' ').some(s => !/^[A-Z]/.test(s))
      );
      
      const chosenOption = terminalOptions.length > 0 
        ? terminalOptions[Math.floor(Math.random() * terminalOptions.length)]
        : options[Math.floor(Math.random() * options.length)];
 
      let result = '';
      for (const part of chosenOption.split(' ')) {
        result += expand(part);
      }
      
      return result;
    };

    return expand('S');
  };

  return (
    <div className="lab8-container">
      <h2 className="lab8-title">Лабораторная работа №8: Генерация текстовой информации</h2>

      <div className="lab8-input-block fade-in">
        <h3>Введите последовательности терминалов (по одной на строку):</h3>
        <textarea
          value={inputSequences}
          onChange={(e) => setInputSequences(e.target.value)}
          className="lab8-textarea"
          placeholder="caaab&#10;bbaab&#10;caab&#10;bbab&#10;cab&#10;bbb&#10;cb"
        />
      </div>

      <button 
        onClick={analyzeSequences}
        disabled={loading}
        className="lab8-button primary"
      >
        {loading ? 'Обработка...' : 'Анализ последовательностей'}
      </button>

      {grammar && (
        <div className="lab8-result-block fade-in">
          <h3>Полученная грамматика:</h3>
          <pre className="lab8-grammar">{JSON.stringify(grammar, null, 2)}</pre>
          
          <button onClick={generateNewSequence} className="lab8-button secondary">
            Сгенерировать новый образ
          </button>
        </div>
      )}

      {generatedSequence && (
        <div className="lab8-output fade-in">
          <h3>Сгенерированная последовательность:</h3>
          <div className="lab8-sequence">{generatedSequence}</div>
        </div>
      )}

    </div>
  );
};

export default Lab8;
