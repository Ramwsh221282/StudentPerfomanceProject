import { Component, Input } from '@angular/core';
import { NgForOf, NgOptimizedImage } from '@angular/common';
import { BlueButtonComponent } from '../../../../building-blocks/buttons/blue-button/blue-button.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-menu-item-block',
  imports: [NgOptimizedImage, NgForOf, BlueButtonComponent],
  templateUrl: './user-menu-item-block.component.html',
  styleUrl: './user-menu-item-block.component.scss',
  standalone: true,
})
export class UserMenuItemBlockComponent {
  @Input({ required: true }) title: string;
  @Input({ required: true }) icon: string;
  @Input({ required: true }) labels: string[];
  @Input({ required: true }) route: string;

  public constructor(private readonly _router: Router) {}

  public navigate(): void {
    this._router.navigate([this.route]);
  }
}
