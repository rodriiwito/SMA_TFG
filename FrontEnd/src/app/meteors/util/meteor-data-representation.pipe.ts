import { Pipe, PipeTransform } from '@angular/core';
import { DataRepresentationsMeteors } from '../models/data-representations-meteors';

@Pipe({
  name: 'meteorDataRepresentation',
  standalone: true,
})
export class DataRepresentationsMeteorsPipe implements PipeTransform {
  transform(value: DataRepresentationsMeteors): string {
    let reportTypeString: string = '';
    switch (value) {
      case DataRepresentationsMeteors.Table:
        reportTypeString = 'Pipes.MeteorDataRepresentation.Table';
        break;
      case DataRepresentationsMeteors.TypePieChart:
        reportTypeString = 'Pipes.MeteorDataRepresentation.TypePieChart';
        break;
    }
    return reportTypeString;
  }
}
