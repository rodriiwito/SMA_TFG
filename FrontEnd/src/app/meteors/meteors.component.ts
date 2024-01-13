import { Component, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableMeteorComponent } from './components/table-meteor/table-meteors.component';
import { DataRepresentationsMeteorsPipe } from './util/meteor-data-representation.pipe';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DataRepresentationsMeteors } from './models/data-representations-meteors';
import { TypeDistributionsComponent } from './components/type-distributions/type-distributions.component';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  standalone: true,
  selector: 'app-meteors',
  templateUrl: './meteors.component.html',
  styleUrls: ['./meteors.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    CommonModule,
    TableMeteorComponent,
    TypeDistributionsComponent,
    DataRepresentationsMeteorsPipe,
    FormsModule,
    ReactiveFormsModule,
    TranslateModule,
  ],
})
export class MeteorsComponent {
  constructor(private formBuilder: FormBuilder) {}

  dataRepresentationTypes: DataRepresentationsMeteors[] = Object.keys(
    DataRepresentationsMeteors
  )
    .map((k) => parseInt(k))
    .filter((k) => !isNaN(k));

  dataRepresentationForm = this.formBuilder.group({
    dataRepresentation: DataRepresentationsMeteors.Table,
  });
}
