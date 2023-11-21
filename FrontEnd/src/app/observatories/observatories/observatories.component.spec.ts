import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ObservatoriesComponent } from './observatories.component';

describe('ObservatoriesComponent', () => {
  let component: ObservatoriesComponent;
  let fixture: ComponentFixture<ObservatoriesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ObservatoriesComponent]
    });
    fixture = TestBed.createComponent(ObservatoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
