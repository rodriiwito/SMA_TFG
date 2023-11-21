import { Component, ChangeDetectionStrategy } from '@angular/core';
import {
  BehaviorSubject,
  Observable,
  combineLatest,
  map,
  startWith,
  switchMap,
} from 'rxjs';
import {
  FormBuilder,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
  MatCommonModule,
  MatNativeDateModule,
} from '@angular/material/core';
import { ReportTypePipe } from '../../util/report-type.pipe';
import { MomentDateAdapter } from '@angular/material-moment-adapter';
import { CommonModule } from '@angular/common';
import { MeteorsService } from 'src/app/services/meteors/meteors.service';
import { MeteorsRequest, MeteorsResponse } from 'src/app/services/models';
import { ReportType } from 'src/app/services/models/enums/report-type';
import { Router } from '@angular/router';

export const MY_FORMATS = {
  display: {
    dateInput: 'DD-MM-YYYY',
    monthYearLabel: 'YYYY',
  },
};

@Component({
  standalone: true,
  selector: 'app-table-meteors',
  templateUrl: './table-meteors.component.html',
  styleUrls: ['./table-meteors.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    CommonModule,
    MatCommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatInputModule,
    ReportTypePipe,
  ],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE],
    },
    { provide: MAT_DATE_FORMATS, useValue: MY_FORMATS },
  ],
})
export class TableMeteorComponent {
  private _mostrarFiltros$ = new BehaviorSubject<boolean>(false);
  private _filtros$ = new BehaviorSubject<MeteorsRequest>({
    minimunDate: null,
    maximumDate: null,
    reportTypes: null,
  });

  reportTypes: ReportType[] = Object.keys(ReportType)
    .map((k) => parseInt(k))
    .filter((k) => !isNaN(k));

  private meteors$ = this._filtros$.pipe(
    switchMap((request) => this.getMeteors(request))
  );

  vm$: Observable<{
    mostrarFiltros: boolean;
    filtros: MeteorsRequest | null;
    meteors: MeteorsResponse | null;
  }> = combineLatest({
    mostrarFiltros: this._mostrarFiltros$,
    filtros: this._filtros$,
    meteors: this.meteors$,
  }).pipe(
    map((x) => x),
    startWith({
      mostrarFiltros: false,
      filtros: null,
      meteors: null,
    })
  );

  meteorsForm = this.formBuilder.group({
    minimunDate: null,
    maximumDate: null,
    reportTypes: new FormControl([]),
  });

  constructor(
    private _meteorsService: MeteorsService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {}

  getMeteors(request: MeteorsRequest): Observable<MeteorsResponse> {
    if (request.minimunDate) {
      request.minimunDate = new Date(request.minimunDate);
      request.minimunDate = new Date(
        Date.UTC(
          request.minimunDate.getFullYear(),
          request.minimunDate.getMonth(),
          request.minimunDate.getDate(),
          0,
          0,
          0,
          0
        )
      );
    }
    if (request.maximumDate) {
      request.maximumDate = new Date(request.maximumDate);
      request.maximumDate = new Date(
        Date.UTC(
          request.maximumDate.getFullYear(),
          request.maximumDate.getMonth(),
          request.maximumDate.getDate(),
          23,
          59,
          59,
          999
        )
      );
    }
    return this._meteorsService.getMeteors(request);
  }

  mostrarFiltros(event: boolean) {
    this._mostrarFiltros$.next(!event);
  }
  aplicarFiltros() {
    this._filtros$.next({
      minimunDate: this.meteorsForm.value.minimunDate!,
      maximumDate: this.meteorsForm.value.maximumDate!,
      reportTypes: this.meteorsForm.value.reportTypes!,
    });
  }

  navigateDetail(id: number) {
    this.router.navigate(['/meteors/', id]).then((err) => {
      console.log(err); // when there's an error
    });
  }
}
