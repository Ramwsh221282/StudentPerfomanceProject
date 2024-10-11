import { Component, OnInit } from '@angular/core';
import { StudentGroupsCreateDataService } from '../../services/student-groups-create-data.service';
import { StudentGroupsDeleteDataService } from '../../services/student-groups-delete-data.service';
import { StudentGroupsFetchDataService } from '../../services/student-groups-fetch-data.service';
import { StudentGroupsMergeDataService } from '../../services/student-groups-merge-data.service';
import { StudentGroupsPaginationService } from '../../services/student-groups-pagination.service';
import { StudentGroupsUpdateDataService } from '../../services/student-groups-update-data.service';
import { StudentGroupsFacadeService } from '../../services/student-groups-facade.service';

@Component({
  selector: 'app-student-groups-page',
  templateUrl: './student-groups-page.component.html',
  styleUrl: './student-groups-page.component.scss',
  providers: [
    StudentGroupsCreateDataService,
    StudentGroupsDeleteDataService,
    StudentGroupsFetchDataService,
    StudentGroupsMergeDataService,
    StudentGroupsPaginationService,
    StudentGroupsUpdateDataService,
    StudentGroupsFacadeService,
  ],
})
export class StudentGroupsPageComponent {}
