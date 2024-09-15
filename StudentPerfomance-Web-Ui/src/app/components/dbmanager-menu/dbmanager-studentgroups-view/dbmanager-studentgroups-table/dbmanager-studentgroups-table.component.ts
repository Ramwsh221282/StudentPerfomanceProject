import { Component, OnInit } from '@angular/core';
import { DbmanagerStudentgroupsPaginationComponent } from './dbmanager-studentgroups-pagination/dbmanager-studentgroups-pagination.component';
import { RouterLink } from '@angular/router';
import { StudentGroupsFacadeService } from '../services/student-groups-facade.service';

@Component({
  selector: 'app-dbmanager-studentgroups-table',
  standalone: true,
  imports: [DbmanagerStudentgroupsPaginationComponent, RouterLink],
  templateUrl: './dbmanager-studentgroups-table.component.html',
  styleUrl: './dbmanager-studentgroups-table.component.scss',
})
export class DbmanagerStudentgroupsTableComponent implements OnInit {
  public constructor(
    protected readonly facadeService: StudentGroupsFacadeService
  ) {}

  public ngOnInit(): void {
    this.facadeService.fetchData();
  }
}
