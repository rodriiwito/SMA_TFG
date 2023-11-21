import { Pipe, PipeTransform } from '@angular/core';
import { ReportType } from 'src/app/services/models/enums/report-type';

@Pipe({
  name: 'reportType',
  standalone: true,
})
export class ReportTypePipe implements PipeTransform {
  transform(value: ReportType): string {
    let reportTypeString: string = '';
    switch (value) {
      case ReportType.DosEstaciones:
        reportTypeString = 'Two Stations';
        break;
      case ReportType.Fotometria:
        reportTypeString = 'Photometry';
        break;
      case ReportType.UnaEstacion:
        reportTypeString = 'One Station';
        break;
    }
    return reportTypeString;
  }
}
