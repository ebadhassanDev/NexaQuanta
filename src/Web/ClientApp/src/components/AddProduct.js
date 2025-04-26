import React, { Component } from 'react';
import axios from 'axios';

export class AddProduct extends Component {
  constructor(props) {
    super(props);
    this.state = {
      name: '',
      price: '',
      description: '',
      category: '',
      quantity: '',
      imageUrl: '',
      loading: false,
      successMessage: '',
      errorMessage: '',
      categoriesFromDb: [],
      loadingCategories: true,
    };
  }

  componentDidMount() {
    this.fetchCategories();
  }

  handleChange = (e) => {
    this.setState({ [e.target.name]: e.target.value });
  }

  handleSubmit = async (e) => {
    e.preventDefault();

    const { name, price, description, category, quantity, imageUrl } = this.state;

    const productData = {
      name,
      price: parseFloat(price),
      description,
      category,
      quantity: parseInt(quantity),
      imageUrl
    };

    try {
      this.setState({ loading: true, successMessage: '', errorMessage: '' });

      await axios.post('/api/product', productData);

      this.setState({
        name: '',
        price: '',
        description: '',
        category: '',
        quantity: '',
        imageUrl: '',
        successMessage: 'Product added successfully!',
      });
    } catch (error) {
      console.error('Error adding product:', error);
      this.setState({ errorMessage: 'Failed to add product.' });
    } finally {
      this.setState({ loading: false });
    }
  }

  render() {
    const {
      name, price, description, category, quantity, imageUrl,
      loading, successMessage, errorMessage, categoriesFromDb, loadingCategories
    } = this.state;

    // Static categories
    const staticCategories = [
      'Electronics',
      'Clothing',
      'Books',
      'Home'
    ];

    return (
      <div className="form-container mt-4">
        <div className="row ">
          <div className="col-12 ">
            <div className="border p-4 rounded shadow-sm">
              <h2>Add New Product</h2>

              {successMessage && <div className="alert alert-success">{successMessage}</div>}
              {errorMessage && <div className="alert alert-danger">{errorMessage}</div>}

              <form onSubmit={this.handleSubmit} className="mt-4">

                <div className="mb-3">
                  <label className="form-label">Product Name</label>
                  <input
                    type="text"
                    className="form-control"
                    name="name"
                    value={name}
                    onChange={this.handleChange}
                    required
                  />
                </div>

                <div className="mb-3">
                  <label className="form-label">Price ($)</label>
                  <input
                    type="number"
                    className="form-control"
                    name="price"
                    value={price}
                    onChange={this.handleChange}
                    step="0.01"
                    required
                  />
                </div>

                <div className="mb-3">
                  <label className="form-label">Quantity</label>
                  <input
                    type="number"
                    className="form-control"
                    name="quantity"
                    value={quantity}
                    onChange={this.handleChange}
                    required
                  />
                </div>

                <div className="mb-3">
                  <label className="form-label">Category</label>
                  {loadingCategories ? (
                    <p>Loading categories...</p>
                  ) : (
                    <select
                      className="form-select"
                      name="category"
                      value={category}
                      onChange={this.handleChange}
                      required
                    >
                      <option value="">Select a category</option>

                      {/* Static categories */}
                      {staticCategories.map((cat, index) => (
                        <option key={`static-${index}`} value={cat}>
                          {cat}
                        </option>
                      ))}

                      {/* Dynamic categories */}
                      {categoriesFromDb.map((cat) => (
                        <option key={`db-${cat.id}`} value={cat.name}>
                          {cat.name}
                        </option>
                      ))}
                    </select>
                  )}
                </div>

                <div className="mb-3">
                  <label className="form-label">Description</label>
                  <textarea
                    className="form-control"
                    name="description"
                    value={description}
                    onChange={this.handleChange}
                    rows="3"
                  ></textarea>
                </div>

                <div className="mb-3">
                  <label className="form-label">Image URL (optional)</label>
                  <input
                    type="text"
                    className="form-control"
                    name="imageUrl"
                    value={imageUrl}
                    onChange={this.handleChange}
                  />
                </div>

                <button type="submit" className="btn btn-primary" disabled={loading}>
                  {loading ? 'Saving...' : 'Add Product'}
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
