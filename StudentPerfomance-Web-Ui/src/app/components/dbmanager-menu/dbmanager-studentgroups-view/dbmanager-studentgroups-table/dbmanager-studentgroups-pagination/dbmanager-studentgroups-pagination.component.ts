import { Component, OnInit } from '@angular/core';
import { NgClass } from '@angular/common';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';

@Component({
  selector: 'app-dbmanager-studentgroups-pagination',
  standalone: true,
  imports: [NgClass],
  templateUrl: './dbmanager-studentgroups-pagination.component.html',
  styleUrl: './dbmanager-studentgroups-pagination.component.scss',
})
export class DbmanagerStudentgroupsPaginationComponent implements OnInit {
  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService
  ) {}
  ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
