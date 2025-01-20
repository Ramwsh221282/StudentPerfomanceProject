import { Component, OnInit } from '@angular/core';
import { IAdminShortInfo } from '../models/admin-short-info.interface';
import { AdminShortInfoService } from '../services/admin-short-info.service';
import { AdminUserDirectionItemComponent } from './admin-user-direction-item/admin-user-direction-item.component';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-admin-user-short-info',
  standalone: true,
  imports: [AdminUserDirectionItemComponent, NgForOf],
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
