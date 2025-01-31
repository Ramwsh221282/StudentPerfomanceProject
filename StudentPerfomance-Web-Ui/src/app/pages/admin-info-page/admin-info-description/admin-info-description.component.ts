import { Component, Input } from '@angular/core';
import { InfoTheme } from '../admin-info-page-section-item-interface';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-admin-info-description',
  imports: [NgForOf],
  templateUrl: './admin-info-description.component.html',
  styleUrl: './admin-info-description.component.scss',
})
export class AdminInfoDescriptionComponent {
  @Input({ required: true }) topic: InfoTheme;
}
