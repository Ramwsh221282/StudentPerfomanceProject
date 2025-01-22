import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../user-page/services/auth.service';
import { AppConfigService } from '../../../../app.config.service';
import { EducationDirection } from '../../../../modules/administration/submodules/education-directions/models/education-direction-interface';
import { DirectionPayloadBuilder } from '../../../../modules/administration/submodules/education-directions/models/contracts/direction-payload-builder';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'any',
})
export class EditEducationDirectionService {
  public constructor(
    private readonly _http: HttpClient,
    private readonly _auth: AuthService,
    private readonly _config: AppConfigService,
  ) {}

  public update(
    initial: EducationDirection,
    updated: EducationDirection,
  ): Observable<EducationDirection> {
    const url = `${this._config.baseApiUri}/api/education-direction`;
    const headers = this.buildHttpHeaders();
    const body = this.buildPayload(initial, updated);
    return this._http.put<EducationDirection>(url, body, {
      headers: headers,
    });
  }

  private buildHttpHeaders(): HttpHeaders {
    return new HttpHeaders().set('token', this._auth.userData.token);
  }

  private buildPayload(
    initial: EducationDirection,
    updated: EducationDirection,
  ): object {
    return {
      initial: DirectionPayloadBuilder(initial),
      updated: DirectionPayloadBuilder(updated),
    };
  }
}
