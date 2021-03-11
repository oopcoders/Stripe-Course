export interface ICustomerPortal {}

export interface ISession {}

export interface IMemberShipPlan {
  id: string;
  priceId: string;
  name: string;
  price: string;
  features: string[];
}
