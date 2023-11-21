import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MeteorsService } from 'src/app/services/meteors/meteors.service';
import { TypeDistributionResponse } from 'src/app/services/models';
import { Observable, combineLatest, map, startWith, tap } from 'rxjs';
import { Labels } from 'src/app/visualizations/models/labels';
import { PieChartComponent } from 'src/app/visualizations/pie-chart/pie-chart.component';
import { BarChartComponent } from 'src/app/visualizations/bar-chart/bar-chart.component';

@Component({
  selector: 'app-type-distributions',
  standalone: true,
  imports: [CommonModule, PieChartComponent, BarChartComponent],
  templateUrl: './type-distributions.component.html',
  styleUrl: './type-distributions.component.css',
})
export class TypeDistributionsComponent {
  private data$ = this.getTypeDistribution();

  vm$: Observable<{
    data: TypeDistributionResponse | null;
    labels: Labels[] | null;
  }> = combineLatest({
    data: this.data$,
  }).pipe(
    map((x) => {
      return {
        data: x.data,
        labels: this.transfromDataToLabels(x.data),
      };
    }),
    startWith({
      data: null,
      labels: null,
    })
  );

  constructor(private _meteorsService: MeteorsService) {}

  getTypeDistribution(): Observable<TypeDistributionResponse> {
    return this._meteorsService.getTypeDistribution();
  }

  transfromDataToLabels(data: TypeDistributionResponse | null): Labels[] {
    return data == null
      ? []
      : [
          { label: 'Only One Station', value: data.onlyOneStation },
          { label: 'Only Two Stations', value: data.onlyTwoStation },
          { label: 'Only Photometry', value: data.onlyPhotometry },
          { label: 'One And Two Station', value: data.oneAndTwoStation },
          {
            label: 'One Station And Photometry',
            value: data.oneStationPhotometry,
          },
          {
            label: 'Two Stations And Photometry',
            value: data.twoStationPhotometry,
          },
          { label: 'All Types', value: data.allTypes },
        ];
  }
}
