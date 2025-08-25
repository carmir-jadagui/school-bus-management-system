import { BoyModel } from "./boyModel";
import { DriverModel } from "./driverModel";

export interface BusModel {
  id: number;
  plate: string;
  brand: string;
  driver: DriverModel;
  boys: BoyModel[];
}