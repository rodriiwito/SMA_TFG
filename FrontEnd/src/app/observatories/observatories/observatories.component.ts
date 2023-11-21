import { Component, ChangeDetectionStrategy } from '@angular/core';
import {
  BehaviorSubject,
  Observable,
  combineLatest,
  map,
  startWith,
  switchMap,
} from 'rxjs';
import { FormBuilder } from '@angular/forms';
import { ObservatoriesService } from 'src/app/services/observatories/observatories.service';
import {
  ObservatoriesRequest,
  ObservatoriesResponse,
} from 'src/app/services/models';

@Component({
  selector: 'app-observatories',
  templateUrl: './observatories.component.html',
  styleUrls: ['./observatories.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ObservatoriesComponent {
  private _mostrarFiltros$ = new BehaviorSubject<boolean>(false);
  private _filtros$ = new BehaviorSubject<ObservatoriesRequest>({
    alturaMaxima: null,
    alturaMinima: null,
    nombre: null,
  });
  private observatories$ = this._filtros$.pipe(
    switchMap((request) => this.getObservatories(request))
  );

  vm$: Observable<{
    mostrarFiltros: boolean;
    filtros: ObservatoriesRequest | null;
    observatories: ObservatoriesResponse | null;
  }> = combineLatest({
    mostrarFiltros: this._mostrarFiltros$,
    filtros: this._filtros$,
    observatories: this.observatories$,
  }).pipe(
    map((x) => x),
    startWith({
      mostrarFiltros: false,
      filtros: null,
      observatories: null,
    })
  );

  observatoriesForm = this.formBuilder.group({
    nombre: null,
    alturaMinima: null,
    alturaMaxima: null,
  });

  constructor(
    private _observatoriesService: ObservatoriesService,
    private formBuilder: FormBuilder
  ) {}

  getObservatories(
    request: ObservatoriesRequest
  ): Observable<ObservatoriesResponse> {
    return this._observatoriesService.getObservatories(request);
  }

  showFilters(event: boolean) {
    this._mostrarFiltros$.next(!event);
  }
  applyFilters() {
    this._filtros$.next({
      alturaMinima: this.observatoriesForm.value.alturaMinima!,
      alturaMaxima: this.observatoriesForm.value.alturaMaxima!,
      nombre: this.observatoriesForm.value.nombre!,
    });
  }
}
