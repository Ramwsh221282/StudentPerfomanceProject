import { Component, OnInit } from '@angular/core';
import { SemesterFacadeService } from '../../services/semester-facade.service';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-semesters-pagination',
  standalone: true,
  imports: [NgClass],
  templateUrl: './semesters-pagination.component.html',
  styleUrl: './semesters-pagination.component.scss',
})
export class SemestersPaginationComponent implements OnInit {
  public constructor(protected readonly facadeService: SemesterFacadeService) {}

  public ngOnInit(): void {
    this.facadeService.refreshPagination();
  }
}
