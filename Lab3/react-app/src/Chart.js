import React, { useEffect, useRef } from 'react';
import { Line } from 'react-chartjs-2';
import { GaussianDistribution } from './GaussianDistribution';
import { Chart, CategoryScale, LinearScale, PointElement, LineElement, Title } from 'chart.js';
Chart.register(CategoryScale, LinearScale, PointElement, LineElement, Title);

const ChartComponent = ({ mean1, stdDev1, mean2, stdDev2, prior1, prior2 }) => {
  const chartRef = useRef(null);
  const dist1 = new GaussianDistribution(mean1, stdDev1);
  const dist2 = new GaussianDistribution(mean2, stdDev2);

  const data = {
    labels: Array.from({ length: 100 }, (_, i) => -10 + i * 0.2),
    datasets: [
      {
        label: 'Класс 1',
        data: Array.from({ length: 100 }, (_, i) => prior1 * dist1.pdf(-10 + i * 0.2)),
        borderColor: 'rgba(75,192,192,1)',
        fill: false,
      },
      {
        label: 'Класс 2',
        data: Array.from({ length: 100 }, (_, i) => prior2 * dist2.pdf(-10 + i * 0.2)),
        borderColor: 'rgba(153,102,255,1)',
        fill: false,
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
          text: 'Ось X', 
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
          text: 'Ось Y', 
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
      <h3>График плотности распределения</h3>
      <Line ref={chartRef} data={data} options={options} />
    </div>
  );
};

export default ChartComponent;