import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseReportContentComponent } from './course-report-content.component';

describe('CourseReportContentComponent', () => {
  let component: CourseReportContentComponent;
  let fixture: ComponentFixture<CourseReportContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CourseReportContentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CourseReportContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
