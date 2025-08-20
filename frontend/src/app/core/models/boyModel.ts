import { PersonBaseModel } from "./personBaseModel";

export interface BoyModel extends PersonBaseModel {
  gender: string;
  age: number;
}