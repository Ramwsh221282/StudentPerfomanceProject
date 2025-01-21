import { Component } from '@angular/core';
import { NgForOf } from '@angular/common';
import { UserMenuItemBlockComponent } from './user-menu-item-block/user-menu-item-block.component';

@Component({
  selector: 'app-admin-user-short-info',
  imports: [NgForOf, UserMenuItemBlockComponent],
  templateUrl: './admin-user-short-info.component.html',
  styleUrl: './admin-user-short-info.component.scss',
  standalone: true,
})
export class AdminUserShortInfoComponent {}
