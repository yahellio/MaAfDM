import React, { useState, useEffect, useCallback } from 'react';
import { GaussianDistribution } from './GaussianDistribution';
import Chart from './Chart';
import './Classification.css'; // Импорт стилей

const Classification = () => {
  const mean1 = 0; // ср знач для кл 1
  const stdDev1 = 1; // станд откл для кл 1
  const mean2 = 2; // ср знач для кл 2
  const stdDev2 = 1; // станд откл для кл 2

  // Состояния 
  const [prior1, setPrior1] = useState(0.5);
  const [prior2, setPrior2] = useState(0.5);

  const [error, setError] = useState({ falseAlarm: 0, miss: 0, totalError: 0 });

  const roundToOneDecimal = (value) => {
    return Math.round(value * 10) / 10;
  };

  const calculateErrors = useCallback(() => {
    const dist1 = new GaussianDistribution(mean1, stdDev1);
    const dist2 = new GaussianDistribution(mean2, stdDev2);

    let falseAlarm = 0;
    let miss = 0;

    for (let x = -10; x <= 10; x += 0.1) {
      const p1 = prior1 * dist1.pdf(x);
      const p2 = prior2 * dist2.pdf(x);

      if (p1 > p2) {
        falseAlarm += p2 * 0.1;
      } else {
        miss += p1 * 0.1;
      }
    }

    setError({
      falseAlarm: falseAlarm.toFixed(6),
      miss: miss.toFixed(6),
      totalError: (falseAlarm + miss).toFixed(6),
    });
  }, [mean1, stdDev1, mean2, stdDev2, prior1, prior2]);

  useEffect(() => {
    calculateErrors();
  }, [calculateErrors]);

  return (
    <div className="classification-container">
      <h3>Classification using Gaussian distributions</h3>

      <div className="input-group">
        <label>
          Prior probability for class 1:
          <input
            type="number"
            step="0.1" 
            value={prior1}
            onChange={(e) => {
              const value = parseFloat(e.target.value);
              if (!isNaN(value)) {
                const roundedValue = roundToOneDecimal(value);
                setPrior1(roundedValue);
                setPrior2(roundToOneDecimal(1 - roundedValue)); 
              }
            }}
          />
        </label>
        
        <label>
          Prior probability for class 2:
          <input
            type="number"
            step="0.1" 
            value={prior2}
            onChange={(e) => {
              const value = parseFloat(e.target.value);
              if (!isNaN(value)) {
                const roundedValue = roundToOneDecimal(value);
                setPrior2(roundedValue);
                setPrior1(roundToOneDecimal(1 - roundedValue)); 
              }
            }}
          />
        </label>
      </div>

      <div className="results">
        <p><strong>False alarm probability:</strong> {error.falseAlarm}</p>
        <p><strong>Missed detection probability:</strong> {error.miss}</p>
        <p><strong>Probability of total classification error:</strong> {error.totalError}</p>
      </div>

      <Chart mean1={mean1} stdDev1={stdDev1} mean2={mean2} stdDev2={stdDev2} prior1={prior1} prior2={prior2} />
    </div>
  );
};

export default Classification;