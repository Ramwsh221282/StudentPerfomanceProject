import { Component, OnInit } from '@angular/core';
import { IAdminShortInfo } from '../models/admin-short-info.interface';
import { AdminShortInfoService } from '../services/admin-short-info.service';
import { NgForOf } from '@angular/common';
import { UserMenuItemBlockComponent } from './user-menu-item-block/user-menu-item-block.component';

@Component({
  selector: 'app-admin-user-short-info',
  standalone: true,
  imports: [NgForOf, UserMenuItemBlockComponent],
  templateUrl: './admin-user-short-info.component.html',
  styleUrl: './admin-user-short-info.component.scss',
})
export class AdminUserShortInfoComponent implements OnInit {
  public adminShortInfo: IAdminShortInfo;

  public constructor(private readonly _service: AdminShortInfoService) {}

  public ngOnInit(): void {
    this._service.invokeGetInfo().subscribe((response) => {
      this.adminShortInfo = response;
    });
  }
}
