import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ObservatoriesComponent } from './observatories/observatories.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [ObservatoriesComponent],
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
})
export class ObservatoriesModule {}
