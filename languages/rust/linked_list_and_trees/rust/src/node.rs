#![allow(dead_code)]

use std::{cell::RefCell, rc::Rc, vec};

// #![allow(unused_variables)]

#[derive(Debug, PartialEq)]
pub struct Node {
    pub value: i32,
    pub next: Option<Rc<RefCell<Node>>>,
}

impl Drop for Node {
    fn drop(&mut self) {
        println!("dropping node: {}", self.value);
    }
}

impl Node {
    fn find_internal(node_cell: Rc<RefCell<Node>>, value: i32) -> Option<Rc<RefCell<Node>>> {
        if node_cell.borrow().value == value {
            return Some(node_cell);
        }

        if let Some(next) = &node_cell.borrow().next {
            let rc_next = Rc::clone(next);
            return Node::find_internal(rc_next, value);
        }

        return None;
    }

    pub fn to_vec(&self) -> Vec<i32> {
        let mut vec = vec![self.value];
        if let Some(next) = &self.next {
            let mut temp = next.borrow().to_vec();
            vec.append(&mut temp);
        };
        vec
    }

    fn last_internal(node: Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        if let Some(next_cell) = &node.borrow().next {
            let next = Rc::clone(&next_cell);
            Node::last_internal(next)
        } else {
            Rc::clone(&node)
        }
    }

    fn push_internal(node: Rc<RefCell<Node>>, value: i32) -> Rc<RefCell<Node>> {
        let last_node = Node::last(&node);
        let new_node = Rc::from(RefCell::from(Node { value, next: None }));
        last_node.borrow_mut().next = Some(Rc::clone(&new_node));
        new_node
    }

    fn pop_internal(node_cell: Rc<RefCell<Node>>) -> Option<Rc<RefCell<Node>>> {
        let mut node = node_cell.borrow_mut();
        let next_node = node.next.as_ref()?;
        let is_last_node = next_node.borrow().next.as_ref().is_none();
        if !is_last_node {
            let next_rc = Rc::clone(next_node);
            return Node::pop_internal(next_rc);
        }

        let popped = node.next.take().unwrap();
        Some(popped)
    }

    fn push_start_internal(node_cell: Rc<RefCell<Node>>, value: i32) -> Rc<RefCell<Node>> {
        let new_node = Rc::from(RefCell::from(Node {
            value,
            next: Some(node_cell),
        }));
        new_node
    }

    fn pop_start_internal(node_cell: Rc<RefCell<Node>>) -> Option<Rc<RefCell<Node>>> {
        let node = node_cell.borrow();
        let next_cell = node.next.as_ref()?;
        Some(Rc::clone(next_cell))
    }

    fn to_vec_ref(node_cell: Rc<RefCell<Node>>) -> Vec<Rc<RefCell<Node>>> {
        let mut vec = vec![Rc::clone(&node_cell)];
        if let Some(next) = node_cell.borrow().next.as_ref() {
            let mut sub_vec = Node::to_vec_ref(Rc::clone(next));
            vec.append(&mut sub_vec);
        }
        vec
    }

    pub fn reverse_internal(node_cell: Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        let vec_ref = Node::to_vec_ref(Rc::clone(&node_cell));
        for (i, vec_cell) in vec_ref.iter().enumerate().skip(1).rev() {
            let prev_index = i - 1;
            let prev = &vec_ref[prev_index];
            let mut node = vec_cell.borrow_mut();
            node.next = Some(Rc::clone(prev));
        }
        node_cell.borrow_mut().next = None;
        let new_start = Rc::clone(vec_ref.last().unwrap());
        new_start
    }
}

impl Node {
    pub fn find(node_cell: &Rc<RefCell<Node>>, value: i32) -> Option<Rc<RefCell<Node>>> {
        Node::find_internal(Rc::clone(node_cell), value)
    }

    pub fn last(node_cell: &Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        Node::last_internal(Rc::clone(node_cell))
    }

    pub fn push(node_cell: &Rc<RefCell<Node>>, value: i32) -> Rc<RefCell<Node>> {
        Node::push_internal(Rc::clone(node_cell), value)
    }

    pub fn pop(node_cell: &Rc<RefCell<Node>>) -> Option<Rc<RefCell<Node>>> {
        Node::pop_internal(Rc::clone(node_cell))
    }

    fn push_start(node_cell: &Rc<RefCell<Node>>, value: i32) -> Rc<RefCell<Node>> {
        Node::push_start_internal(Rc::clone(node_cell), value)
    }

    fn pop_start(node_cell: &Rc<RefCell<Node>>) -> Option<Rc<RefCell<Node>>> {
        Node::pop_start_internal(Rc::clone(node_cell))
    }

    fn reverse(node_cell: &Rc<RefCell<Node>>) -> Rc<RefCell<Node>> {
        Node::reverse_internal(Rc::clone(node_cell))
    }
}

fn get_test_list(size: i32, current: i32) -> Rc<RefCell<Node>> {
    let mut node = Node {
        value: current,
        next: None,
    };
    if current < size {
        let next = get_test_list(size, current + 1);
        node.next = Some(next);
    }
    Rc::from(RefCell::new(node))
}

#[cfg(test)]
mod tests {
    use crate::node::*;

    #[test]
    fn test_to_vec() {
        let root = get_test_list(5, 1);
        let vec = root.borrow().to_vec();

        assert_eq!(vec.len(), 5);
        assert_eq!(vec[0], 1);
        assert_eq!(vec[1], 2);
        assert_eq!(vec[2], 3);
        assert_eq!(vec[3], 4);
        assert_eq!(vec[4], 5);
    }

    #[test]
    fn test_find() {
        let root = get_test_list(5, 1);
        assert_eq!(Node::find(&root, 1).unwrap().borrow().value, 1);
        assert_eq!(Node::find(&root, 3).unwrap().borrow().value, 3);
        assert_eq!(Node::find(&root, 0), None);
    }

    #[test]
    fn test_push() {
        let root_cell = get_test_list(3, 1);
        let old_last_node = Node::last(&root_cell);
        let new_node = Node::push(&root_cell, 4);
        let last_node = Node::last(&root_cell);

        let last_node_ptr: *const Node = &*last_node.borrow();
        let new_node_ptr: *const Node = &*new_node.borrow();
        assert_ne!(old_last_node, new_node);
        assert_eq!(last_node, new_node);
        assert_eq!(last_node_ptr, new_node_ptr);
        assert_eq!(new_node.borrow().value, 4);
    }

    #[test]
    fn test_pop() {
        let root_cell = get_test_list(3, 1);
        println!("{:?}", root_cell);
        let popped = Node::pop(&root_cell);
        println!("{:?}", root_cell);
        println!("{:?}", popped);

        assert_eq!(root_cell.borrow().to_vec(), [1, 2]);
        assert_eq!(popped.unwrap().borrow().to_vec(), [3]);
    }

    #[test]
    fn test_push_start() {
        let root_cell = get_test_list(4, 1);
        println!("{:?}", root_cell);
        let pushed = Node::push_start(&root_cell, 5);
        println!("{:?}", root_cell.borrow().to_vec());
        println!("{:?}", pushed);

        assert_eq!(root_cell.borrow().to_vec(), [1, 2, 3, 4]);
        assert_eq!(pushed.borrow().to_vec(), [5, 1, 2, 3, 4]);
    }

    #[test]
    fn test_pop_start() {
        let old_list = get_test_list(5, 1);
        println!("{:?}", old_list.borrow().to_vec());
        let new_list = Node::pop_start(&old_list).unwrap();
        println!("{:?}", old_list.borrow().to_vec());
        println!("{:?}", new_list.borrow().to_vec());

        assert_eq!(old_list.borrow().to_vec(), [1, 2, 3, 4, 5]);
        assert_eq!(new_list.borrow().to_vec(), [2, 3, 4, 5]);
    }

    #[test]
    fn test_reverse() {
        let old_list = get_test_list(5, 1);
        println!("{:?}", old_list.borrow().to_vec());
        let new_list = Node::reverse(&old_list);
        println!("{:?}", old_list.borrow().to_vec());
        println!("{:?}", new_list.borrow().to_vec());

        assert_eq!(old_list.borrow().to_vec(), [1]);
        assert_eq!(new_list.borrow().to_vec(), [5, 4, 3, 2, 1]);
    }
}
