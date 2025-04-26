import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { AddProduct } from './AddProduct';

const ProductList = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showModal, setShowModal] = useState(false);
  useEffect(() => {
    fetchProducts();
  }, []);

  const fetchProducts = async () => {
    try {
      const response = await axios.get('/api/product');
      setProducts(response.data);
    } catch (err) {
      console.error('Error fetching products:', err);
      setError('Failed to fetch products.');
    } finally {
      setLoading(false);
    }
  };

  const toggleModal = () => {
    setShowModal(!showModal);
  };

  const handleDelete = async (productId) => {
    if (window.confirm('Are you sure you want to delete this product?')) {
      try {
        await axios.delete(`/api/product/${productId}`);
        setProducts(products.filter((product) => product.id !== productId));
      } catch (err) {
        console.error('Error deleting product:', err);
        setError('Failed to delete product.');
      }
    }
  };

  if (loading) {
    return <div>Loading products...</div>;
  }

  if (error) {
    return <div className="alert alert-danger">{error}</div>;
  }

  return (
    <div className="container mt-5">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Product List</h2>

        <div className="mb-4">
          <button className="btn btn-primary" onClick={toggleModal}>
            Add New Product
          </button>
        </div>
      </div>

      {/* Modal */}
      {showModal && (
        <div
          className="modal fade show"
          style={{ display: 'block' }}
          tabIndex="-1"
          role="dialog"
          aria-hidden="false"
        >
          <div className="modal-dialog modal-dialog-centered" role="document" style={{ maxWidth: '700px' }}>
            <div className="modal-content">
              <div className="modal-header">
                <h5 className="modal-title">Add New Product</h5>
                <button
                  type="button"
                  className="close"
                  onClick={toggleModal}
                  aria-label="Close"
                >
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div className="modal-body">
                <AddProduct className="w-100" />
              </div>
            </div>
          </div>
        </div>
      )}

      {/* Background overlay */}
      {showModal && (
        <div
          className="modal-backdrop fade show"
          onClick={toggleModal}
        ></div>
      )}

      {/* Product Table */}
      <div className="table-responsive">
        <table className="table table-striped">
          <thead>
            <tr>
              <th>Name</th>
              <th>Price</th>
              <th>Date Added</th>
              <th>Quantity</th>
              <th>Description</th>
              <th>Image</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {products.length === 0 ? (
              <tr>
                <td colSpan="7" className="text-center">No products found.</td>
              </tr>
            ) : (
              products.map((product) => (
                <tr key={product.id}>
                  <td>{product.name}</td>
                  <td>${product.price}</td>
                  <td>{new Date(product.dateAdded).toLocaleDateString()}</td>
                  <td>{product.quantity}</td>
                  <td>{product.description}</td>
                  <td>
                    <img src={product.imageUrl} alt={product.name} width="50" />
                  </td>
                  <td className="d-flex gap-1">
                    <button className="btn btn-warning btn-sm mr-2">Edit</button>
                    <button className="btn btn-danger btn-sm" onClick={() => handleDelete(product.id)}>Delete</button>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default ProductList;
