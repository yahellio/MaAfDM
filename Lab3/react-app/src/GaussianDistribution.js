export class GaussianDistribution {
    constructor(mean, stdDev) {
      this.mean = mean;
      this.stdDev = stdDev;
    }
  
    pdf(x) {
      const variance = this.stdDev * this.stdDev;
      const exponent = -Math.pow(x - this.mean, 2) / (2 * variance);
      return (1 / (Math.sqrt(2 * Math.PI * variance))) * Math.exp(exponent);
    }
}