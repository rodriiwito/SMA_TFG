import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-meteor-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './meteor-detail.component.html',
  styleUrl: './meteor-detail.component.css',
})
export class MeteorDetailComponent {
  id: string | null = this.route.snapshot.paramMap.get('id');
  constructor(private route: ActivatedRoute) {}
}
