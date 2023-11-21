import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TypeDistributionsComponent } from './type-distributions.component';

describe('TypeDistributionsComponent', () => {
  let component: TypeDistributionsComponent;
  let fixture: ComponentFixture<TypeDistributionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TypeDistributionsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TypeDistributionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
