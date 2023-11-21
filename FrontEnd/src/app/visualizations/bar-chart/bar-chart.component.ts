import { Component, Input, OnChanges } from '@angular/core';
import { TypeDistributionResponse } from 'src/app/services/models';
import * as d3 from 'd3';
import { Labels } from '../models/labels';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  imports: [CommonModule],
})
export class BarChartComponent implements OnChanges {
  @Input() data: Labels[] = [];

  xScale = d3.scaleBand().range([30, 190]);
  yScale = d3.scaleLinear().range([180, 20]);
  colorScale = d3.scaleOrdinal(d3.schemeCategory10);

  ngOnChanges() {
    this.xScale.domain(this.data.map((d) => d.label));
    this.yScale.domain([
      0,
      Math.max.apply(
        null,
        this.data.map((x) => x.value)
      ),
    ]);
  }
}
