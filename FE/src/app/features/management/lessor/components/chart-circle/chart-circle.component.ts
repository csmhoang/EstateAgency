import { Component, inject, PLATFORM_ID, ViewChild } from '@angular/core';
import {
  ApexChart,
  ApexNonAxisChartSeries,
  ApexResponsive,
  ChartComponent,
  NgApexchartsModule,
} from 'ng-apexcharts';
import { CommonModule, isPlatformBrowser } from '@angular/common';

export type ChartOptions = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

@Component({
  selector: 'app-chart-circle',
  standalone: true,
  imports: [CommonModule, NgApexchartsModule],
  templateUrl: './chart-circle.component.html',
  styleUrl: './chart-circle.component.scss',
})
export class ChartCircleComponent {
  @ViewChild('chart') chart?: ChartComponent;
  public chartOptions?: Partial<ChartOptions>;
  platformId = inject(PLATFORM_ID);

  constructor() {
    if (isPlatformBrowser(this.platformId)) {
      this.chartOptions = {
        series: [44, 55, 13, 43, 22],
        chart: {
          width: 380,
          type: 'pie',
        },
        labels: ['Team A', 'Team B', 'Team C', 'Team D', 'Team E'],
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200,
              },
              legend: {
                position: 'bottom',
              },
            },
          },
        ],
      };
    }
  }
}
