import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SemesterFacadeService } from '../services/semester-facade.service';
import { SemestersPaginationComponent } from './semesters-pagination/semesters-pagination.component';

@Component({
  selector: 'app-dbmanager-semesters-table',
  standalone: true,
  imports: [RouterLink, SemestersPaginationComponent],
  templateUrl: './dbmanager-semesters-table.component.html',
  styleUrl: './dbmanager-semesters-table.component.scss',
})
export class DbmanagerSemestersTableComponent implements OnInit {
  public constructor(protected readonly facadeService: SemesterFacadeService) {}
  public ngOnInit(): void {
    this.facadeService.fetch();
  }
}
