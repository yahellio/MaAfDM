import React, { useEffect, useRef } from 'react';
import { Line } from 'react-chartjs-2';
import { GaussianDistribution } from './GaussianDistribution';
import { Chart, CategoryScale, LinearScale, PointElement, LineElement, Title } from 'chart.js';
Chart.register(CategoryScale, LinearScale, PointElement, LineElement, Title);

const ChartComponent = ({ mean1, stdDev1, mean2, stdDev2, prior1, prior2 }) => {
  const chartRef = useRef(null);
  const dist1 = new GaussianDistribution(mean1, stdDev1);
  const dist2 = new GaussianDistribution(mean2, stdDev2);

  // Функция для нахождения точек пересечения графиков плотности
  const findIntersectionPoints = () => {
    const xRange = Array.from({ length: 100 }, (_, i) => -10 + i * 0.2);
    let intersections = [];

    for (let i = 0; i < xRange.length - 1; i++) {
      const x1 = xRange[i];
      const x2 = xRange[i + 1];
      const y1_1 = prior1 * dist1.pdf(x1);
      const y2_1 = prior2 * dist2.pdf(x1);
      const y1_2 = prior1 * dist1.pdf(x2);
      const y2_2 = prior2 * dist2.pdf(x2);

      if ((y1_1 > y2_1 && y1_2 < y2_2) || (y1_1 < y2_1 && y1_2 > y2_2)) {
        // Пересечение найдено, находим более точное значение
        const xIntersection = (x1 + x2) / 2; // Простейший способ нахождения
        intersections.push(xIntersection);
      }
    }

    return intersections;
  };

  // Найти точки пересечения
  let intersections = findIntersectionPoints();

  // Если пересечений нет (например, при равных приоритетах), установим вертикальную линию по центру графика
  if (intersections.length === 0 && prior1 === prior2) {
    intersections = [1];  // Установим вертикальную линию на оси X = 0
  }

  // Генерация данных для вертикальной линии
  const verticalLineData = intersections.flatMap(x => {
    return Array.from({ length: 10 }, (_, i) => ({
      x: x,  // фиксированное значение для вертикальной линии
      y: (i / 10) * 0.5,  // Генерируем значения y от 0 до 0.5 для линии
    }));
  });

  const data = {
    labels: Array.from({ length: 100 }, (_, i) => -10 + i * 0.2),
    datasets: [
      {
        label: 'Класс 1',
        data: Array.from({ length: 100 }, (_, i) => prior1 * dist1.pdf(-10 + i * 0.2)),
        borderColor: 'rgb(252, 0, 0)',
        fill: false,
      },
      {
        label: 'Класс 2',
        data: Array.from({ length: 100 }, (_, i) => prior2 * dist2.pdf(-10 + i * 0.2)),
        borderColor: 'rgb(21, 255, 0)',
        fill: false,
      },
      {
        label: 'Пересечения',
        data: verticalLineData,  // Передаем данные для вертикальной линии
        borderColor: 'rgb(214, 214, 214)',  // цвет вертикальной линии
        borderWidth: 2,
        fill: false,
        pointRadius: 0,  // Убираем точки
        lineTension: 0,  // Делаем линию прямой
      },
    ],
  };

  const options = {
    scales: {
      x: {
        type: 'linear', 
        min: -5, 
        max: 5,  
        ticks: {
          stepSize: 1, 
          precision: 0, 
        },
        title: {
          display: true,
          text: 'X', 
        },
        grid: {
          display: true, 
        },
      },
      y: {
        min: 0,   
        max: 0.5, 
        ticks: {
          stepSize: 0.1, 
          precision: 1,  
        },
        title: {
          display: true,
          text: 'Y', 
        },
        grid: {
          display: true, 
        },
      },
    },
    plugins: {
      legend: {
        display: true, 
      },
    },
  };

  useEffect(() => {
    const chart = chartRef.current;

    return () => {
      if (chart) {
        chart.destroy(); 
      }
    };
  }, []);

  return (
    <div>
      <h3>Density distribution graph</h3>
      <Line ref={chartRef} data={data} options={options} />
    </div>
  );
};

export default ChartComponent;
