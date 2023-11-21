import { Component, Input, OnChanges } from '@angular/core';
import * as d3 from 'd3';
import { Labels } from '../models/labels';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.css'],
  imports: [CommonModule],
})
export class PieChartComponent implements OnChanges {
  @Input('pieData') pieData: Labels[] = [];
  pieArcData: any;
  arcPie: any;
  colorScale = d3.scaleOrdinal(d3.schemeCategory10);
  totalNumber: number = 0;
  ngOnChanges() {
    const pieArc: any = d3.pie().value((d: any) => d.value);
    this.pieArcData = pieArc(this.pieData);
    this.arcPie = d3.arc().innerRadius(0).outerRadius(80);
    this.totalNumber = this.pieData.reduce((x, a) => x + a.value, 0);
  }
}
