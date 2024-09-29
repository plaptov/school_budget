import { BaseEntity } from "./base-entity";
import { FundraisingEditDto } from "./fundraising-edit-dto";

export enum FundraisingType {
  OneTime = 'OneTime',
  Periodic = 'Periodic',
}

export interface Fundraising extends FundraisingEditDto, BaseEntity {
  fundId: number;
  type: FundraisingType;
  isClosed: boolean;
  closingDate?: Date;
}
