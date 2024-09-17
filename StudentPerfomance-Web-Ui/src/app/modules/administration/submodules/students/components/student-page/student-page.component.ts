import { Component, OnInit } from '@angular/core';
import { FacadeStudentService } from '../../services/facade-student.service';
import { ActivatedRoute } from '@angular/router';
import { StudentGroup } from '../../../student-groups/services/studentsGroup.interface';

@Component({
  selector: 'app-student-page',
  templateUrl: './student-page.component.html',
  styleUrl: './student-page.component.scss',
})
export class StudentPageComponent implements OnInit {
  public constructor(
    protected readonly facadeService: FacadeStudentService,
    private readonly _activatedRoute: ActivatedRoute
  ) {}

  public ngOnInit(): void {
    this._activatedRoute.params.subscribe((params) => {
      const group: StudentGroup = {
        groupName: params['groupName'],
      } as StudentGroup;
      this.facadeService.setStudentGroup(group);
    });
  }
}
