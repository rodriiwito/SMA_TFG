import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObservatoriosComponent } from './observatorios.component';

describe('ObservatoriosComponent', () => {
  let component: ObservatoriosComponent;
  let fixture: ComponentFixture<ObservatoriosComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ObservatoriosComponent]
    });
    fixture = TestBed.createComponent(ObservatoriosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
