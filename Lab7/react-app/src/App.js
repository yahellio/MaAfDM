import React, { useState, useRef, useEffect } from 'react';
import './App.css';

function App() {
  const [selectedElement, setSelectedElement] = useState(null);
  const [elements, setElements] = useState([]);
  const [error, setError] = useState('');
  const [result, setResult] = useState('');
  const canvasRef = useRef(null);

  const faceElements = {
    'eye': { emoji: '👁️', description: 'Глаза (обязательно)' },
    'nose': { emoji: '👃', description: 'Нос (необязательно)' },
    'mouth': { emoji: '👄', description: 'Рот (обязательно)' },
    'hat': { emoji: '🎩', description: 'Шляпа (необязательно)' }
  };

  // Грамматика для разных типов лиц
  const grammar = {
    'Face': [
      ['eyes', 'mouth'],                 // Только глаза и рот
      ['eyes', 'nose', 'mouth'],        // Глаза, нос и рот
      ['hat', 'eyes', 'mouth'],         // Шляпа, глаза и рот
      ['hat', 'eyes', 'nose', 'mouth']  // Все элементы
    ],
    'eyes': [['eye', 'eye']],          // Глаза всегда парой
  };

  useEffect(() => {
    const canvas = canvasRef.current;
    const ctx = canvas.getContext('2d');
    ctx.clearRect(0, 0, canvas.width, canvas.height);
    
    // Рисуем контур лица
    ctx.beginPath();
    ctx.arc(150, 150, 100, 0, 2 * Math.PI);
    ctx.strokeStyle = '#000';
    ctx.lineWidth = 2;
    ctx.stroke();
    
    // Рисуем все элементы
    elements.forEach(el => {
      ctx.font = '30px Arial';
      ctx.fillText(el.emoji, el.x - 15, el.y + 10);
    });
  }, [elements]);

  const handleCanvasClick = (e) => {
    if (!selectedElement) return;
    
    const canvas = canvasRef.current;
    const rect = canvas.getBoundingClientRect();
    const x = e.clientX - rect.left;
    const y = e.clientY - rect.top;
    
    // Для глаз добавляем сразу пару
    if (selectedElement === 'eye') {
      setElements([
        ...elements,
        { type: 'eye', emoji: faceElements.eye.emoji, x: x - 30, y: y },
        { type: 'eye', emoji: faceElements.eye.emoji, x: x + 30, y: y }
      ]);
    } else {
      setElements([
        ...elements,
        { type: selectedElement, emoji: faceElements[selectedElement].emoji, x, y }
      ]);
    }
    
    setError('');
    setResult('');
  };

  const validateFace = () => {
    setError('');
    setResult('');
    
  // Сортируем элементы по вертикали (сверху вниз)  
  const sortedElements = [...elements].sort((a, b) => a.y - b.y);

  // Создаем последовательность типов элементов в порядке сверху вниз
  const sequence = sortedElements.map(el => el.type);
  
    // Проверяем по грамматике
    try {
      const remaining = tryParse(sequence, 'Face');

      if (remaining !== null && remaining.length === 0) {
        setResult('Лицо составлено правильно! Тип: ' + getFaceType(sequence));
      } else {
        setError('Лицо не соответствует правилам!');
      }
    } catch (err) {
      setError('Ошибка при проверке лица: ' + err.message);
    }
  };
  
  const tryParse = (sequence, symbol) => {
    if (symbol === sequence[0]) return sequence.slice(1);
    
    for (const rule of grammar[symbol] || []) {
      let remainingSequence = [...sequence];
      let allMatched = true;
      
      for (const part of rule) {
        const result = tryParse(remainingSequence, part);
        if (!result) {
          allMatched = false;
          break;
        }
        remainingSequence = result;
      }
      
      if (allMatched) return remainingSequence;
    }
    
    return null;
  };

  const getFaceType = (sequence) => {
    const hasHat = sequence.includes('hat');
    const hasNose = sequence.includes('nose');
    
    if (hasHat && hasNose) return 'Полное лицо (со шляпой и носом)';
    if (hasHat) return 'Лицо со шляпой';
    if (hasNose) return 'Лицо с носом';
    return 'Базовое лицо';
  };

  const clearCanvas = () => {
    setElements([]);
    setError('');
    setResult('');
  };

  return (
    <div className="App">
      <h1>Распознавание лица с помощью синтаксического метода</h1>
      
      <div className="controls">
        <h3>Выберите элемент:</h3>
        <div className="element-buttons">
          {Object.entries(faceElements).map(([key, value]) => (
            <button
              key={key}
              className={selectedElement === key ? 'selected' : ''}
              onClick={() => setSelectedElement(key)}
            >
              {value.emoji} {value.description}
            </button>
          ))}
        </div>
        
        <div className="action-buttons">
          <button onClick={validateFace}>Проверить лицо</button>
          <button onClick={clearCanvas}>Очистить</button>
        </div>
      </div>
      
      <div className="canvas-container">
        <canvas
          ref={canvasRef}
          width="300"
          height="300"
          onClick={handleCanvasClick}
        />
      </div>
      
      {error && <div className="error">{error}</div>}
      {result && <div className="result">{result}</div>}
      
      <div className="rules">
        <h3>Правила составления лица:</h3>
        <ul>
          <li>Глаза (👁️) - обязательный элемент (добавляются парой)</li>
          <li>Рот (👄) - обязательный элемент</li>
          <li>Нос (👃) - необязательный элемент</li>
          <li>Шляпа (🎩) - необязательный элемент</li>
          <li>Порядок: шляпа (если есть) → глаза → нос (если есть) → рот</li>
        </ul>
      </div>
    </div>
  );
}

export default App;