import { AddProduct } from "./components/AddProduct";
import { Counter } from "./components/Counter";
import { Home } from "./components/Home";
import ProductList from "./components/product-list";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: 'add-product',
    element: <AddProduct />
  },
  {
    path: 'product-list',
    element: <ProductList />
  }
];

export default AppRoutes;
