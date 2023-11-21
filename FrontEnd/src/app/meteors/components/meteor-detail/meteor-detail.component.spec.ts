import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeteorDetailComponent } from './meteor-detail.component';

describe('MeteorDetailComponent', () => {
  let component: MeteorDetailComponent;
  let fixture: ComponentFixture<MeteorDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MeteorDetailComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MeteorDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
