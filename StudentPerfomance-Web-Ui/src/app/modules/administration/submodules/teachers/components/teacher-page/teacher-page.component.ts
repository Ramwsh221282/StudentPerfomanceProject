import { Component, OnInit } from '@angular/core';
import { FetchTeacherService } from '../../services/fetch-teacher.service';
import { FacadeTeacherService } from '../../services/facade-teacher.service';
import { ActivatedRoute } from '@angular/router';
import { Department } from '../../../departments/models/departments.interface';

@Component({
  selector: 'app-teacher-page',
  templateUrl: './teacher-page.component.html',
  styleUrl: './teacher-page.component.scss',
})
export class TeacherPageComponent implements OnInit {
  protected readonly fetchService: FetchTeacherService;

  public constructor(
    protected readonly facadeTeacherService: FacadeTeacherService,
    private readonly _activatedRoute: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      const departmentName = params['departmentName'];
      const department = { name: departmentName } as Department;
      this.facadeTeacherService.setDepartment(department);
    });
  }
}
