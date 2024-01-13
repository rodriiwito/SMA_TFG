import { Pipe, PipeTransform } from '@angular/core';
import { ReportType } from 'src/app/services/models/enums/report-type';

@Pipe({
  name: 'reportType',
  standalone: true,
})
export class ReportTypePipe implements PipeTransform {
  transform(value: ReportType): string {
    let reportTypeString: string = 'Pipes.ReportTypePipe.';
    switch (value) {
      case ReportType.DosEstaciones:
        reportTypeString += 'TwoStations';
        break;
      case ReportType.Fotometria:
        reportTypeString += 'Photometry';
        break;
      case ReportType.UnaEstacion:
        reportTypeString += 'OneStation';
        break;
    }
    return reportTypeString;
  }
}
