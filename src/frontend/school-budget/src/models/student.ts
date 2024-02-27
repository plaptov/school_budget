import { BaseEntity } from "./base-entity";

export interface Student extends BaseEntity {
  name: string;
  comment?: string;
  adults: number[];
}
