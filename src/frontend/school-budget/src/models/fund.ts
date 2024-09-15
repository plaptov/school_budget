import { BaseEntity } from "./base-entity";

export enum FundType
{
    Default = 'Default',
    Targeted = 'Targeted',
    Continuous = 'Continuous',
}

export interface Fund extends BaseEntity {
  name: string;
  type: string;
  isClosed: boolean;

  canClose: boolean;
}
