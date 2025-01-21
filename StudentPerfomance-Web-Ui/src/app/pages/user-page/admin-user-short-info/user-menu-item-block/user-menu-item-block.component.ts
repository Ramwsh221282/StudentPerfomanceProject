import { Component, Input } from '@angular/core';
import { NgForOf, NgOptimizedImage } from '@angular/common';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';

@Component({
  selector: 'app-user-menu-item-block',
  standalone: true,
  imports: [NgOptimizedImage, NgForOf, BlueButtonComponent],
  templateUrl: './user-menu-item-block.component.html',
  styleUrl: './user-menu-item-block.component.scss',
})
export class UserMenuItemBlockComponent {
  @Input({ required: true }) title: string;
  @Input({ required: true }) icon: string;
  @Input({ required: true }) labels: string[];
}
