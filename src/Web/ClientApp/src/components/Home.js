import React, { Component } from 'react';
import { AddProduct } from './AddProduct';
import ProductList from './product-list';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = {
      selectedPage: 'dashboard',
    };
  }

  handleMenuClick = (page) => {
    this.setState({ selectedPage: page });
  };

  renderContent() {
    const { selectedPage } = this.state;

    if (selectedPage === 'dashboard') {
      return (
        <div className="container-fluid mt-4">
          <h1 className="mb-4">Dashboard</h1>

          {/* Statistic Cards */}
          <div className="row">
            <div className="col-md-4 mb-4">
              <div className="card shadow-sm">
                <div className="card-body">
                  <h5 className="card-title">Total Products</h5>
                  <p className="card-text fs-3">120</p>
                </div>
              </div>
            </div>

            <div className="col-md-4 mb-4">
              <div className="card shadow-sm">
                <div className="card-body">
                  <h5 className="card-title">Total Sales</h5>
                  <p className="card-text fs-3">$4,500</p>
                </div>
              </div>
            </div>

            <div className="col-md-4 mb-4">
              <div className="card shadow-sm">
                <div className="card-body">
                  <h5 className="card-title">Total Users</h5>
                  <p className="card-text fs-3">78</p>
                </div>
              </div>
            </div>
          </div>

          {/* Recent Products */}
          <div className="card shadow-sm mb-5">
            <div className="card-body">
              <h5 className="card-title mb-4">Recent Products</h5>

              <div className="table-responsive">
                <table className="table table-hover">
                  <thead className="table-light">
                    <tr>
                      <th>Product</th>
                      <th>Price</th>
                      <th>Date Added</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>Product A</td>
                      <td>$50</td>
                      <td>April 25, 2025</td>
                    </tr>
                    <tr>
                      <td>Product B</td>
                      <td>$80</td>
                      <td>April 24, 2025</td>
                    </tr>
                    <tr>
                      <td>Product C</td>
                      <td>$30</td>
                      <td>April 23, 2025</td>
                    </tr>
                  </tbody>
                </table>
              </div>

            </div>
          </div>
        </div>
      );
    }
    else if (selectedPage === 'product-add') {
      return <AddProduct />;
    }
    else if (selectedPage === 'product-list') {
      return <ProductList />;
    }
  }

  render() {
    return (
      <div className="container-fluid">
        <main className="col-md-12 px-md-4">
          {this.renderContent()}
        </main>
      </div>
    );
  }
}
