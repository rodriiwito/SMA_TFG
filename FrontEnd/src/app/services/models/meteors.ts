import { BaseResponse } from './base-response';
import { ReportType } from './enums/report-type';

export interface MeteorsRequest {
  minimunDate: Date | null;
  maximumDate: Date | null;
  reportTypes: ReportType[] | null;
}

export interface MeteorsResponse extends BaseResponse {
  meteors: Meteor[];
}

export interface Meteor {
  id: number;
  date: string;
  twoStationsReports: number;
  oneStationReports: number;
  photometryReports: number;
}

export interface TypeDistributionResponse extends BaseResponse {
  onlyOneStation: number;
  onlyTwoStation: number;
  onlyPhotometry: number;
  oneAndTwoStation: number;
  oneStationPhotometry: number;
  twoStationPhotometry: number;
  allTypes: number;
}
