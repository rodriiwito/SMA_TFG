import { Component, ChangeDetectionStrategy } from '@angular/core';
import { ConsultasService } from 'src/app/common/consultas/consultas.service';
import {
  ObservatoriosRequest,
  ObservatoriosResponse,
} from 'src/app/common/consultas/models';
import {
  BehaviorSubject,
  Observable,
  combineLatest,
  map,
  startWith,
  switchMap,
} from 'rxjs';
import { FormsModule, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-observatorios',
  templateUrl: './observatorios.component.html',
  styleUrls: ['./observatorios.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ObservatoriosComponent {
  private _mostrarFiltros$ = new BehaviorSubject<boolean>(false);
  private _filtros$ = new BehaviorSubject<ObservatoriosRequest>({
    alturaMaxima: null,
    alturaMinima: null,
    nombre: null,
  });
  private observatorios$ = this._filtros$.pipe(
    switchMap((request) => this.obtenerObservatorios(request))
  );

  vm$: Observable<{
    mostrarFiltros: boolean;
    filtros: ObservatoriosRequest | null;
    observatorios: ObservatoriosResponse | null;
  }> = combineLatest({
    mostrarFiltros: this._mostrarFiltros$,
    filtros: this._filtros$,
    observatorios: this.observatorios$,
  }).pipe(
    map((x) => x),
    startWith({
      mostrarFiltros: false,
      filtros: null,
      observatorios: null,
    })
  );

  observatorioForm = this.formBuilder.group({
    nombre: null,
    alturaMinima: null,
    alturaMaxima: null,
  });

  constructor(
    private _consultasService: ConsultasService,
    private formBuilder: FormBuilder
  ) {}

  obtenerObservatorios(
    request: ObservatoriosRequest
  ): Observable<ObservatoriosResponse> {
    return this._consultasService.getObservatorios(request);
  }

  mostrarFiltros(event: boolean) {
    this._mostrarFiltros$.next(!event);
  }
  aplicarFiltros() {
    this._filtros$.next({
      alturaMinima: this.observatorioForm.value.alturaMinima!,
      alturaMaxima: this.observatorioForm.value.alturaMaxima!,
      nombre: this.observatorioForm.value.nombre!,
    });
  }
}
